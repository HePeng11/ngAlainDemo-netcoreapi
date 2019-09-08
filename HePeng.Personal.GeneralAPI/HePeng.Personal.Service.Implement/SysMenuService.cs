using HePeng.Personal.Common.Consts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.Menus;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HePeng.Personal.Service.Implement
{
    public class SysMenuService : BaseService<SysMenu, SysMenuQueryParam>, ISysMenuService
    {
        readonly ISysMenuRepository SysMenuRepository;
        readonly ISysObjectAclService SysObjectAclService;
        IMemoryCache Cache { get; }
        public SysMenuService(ISysMenuRepository sysMenuRepository, ISysObjectAclService sysObjectAclService, IMemoryCache cache)
        {
            Repository = SysMenuRepository = sysMenuRepository;
            SysObjectAclService = sysObjectAclService;
            Cache = cache;
        }

        /// <summary>
        /// 获取所有菜单集合
        /// </summary>
        /// <returns></returns>
        private List<SysMenu> GetAllMenus()
        {
            var menus = Cache.GetOrCreate(CacheKeys.SysAllMenu, (a) =>
            {
                a.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CommonConst.CacheMinutes);
                var list = SysMenuRepository.GetAllMenus();
                return list;
            });
            return menus;
        }

        #region
        /// <summary>
        /// 当前人获取有权限的菜单
        /// </summary>
        /// <returns></returns>
        public List<NgMenu> GetMenus()
        {
            var menus = Cache.GetOrCreate(CacheKeys.SysMenu.Format(CurrentUser.Id), (a) =>
            {
                a.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CommonConst.CacheMinutes);
                var list = GetAllMenus();
                return GetLevelMenus(list);
            });

            //组装
            //MemoryCacheEntryOptions options =new MemoryCacheEntryOptions();
            //options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            //options.SlidingExpiration = TimeSpan.FromMinutes(1);
            ////options.RegisterPostEvictionCallback(MyCallback, this); //设置回调函数
            //Cache.Set<string>("timestamp", DateTime.Now.ToString(), options);

            return menus;
        }



        /// <summary>
        /// 组装菜单层级数据
        /// </summary>
        /// <param name="sysMenus"></param>
        private List<NgMenu> GetLevelMenus(List<SysMenu> sysMenus)
        {
            List<NgMenu> ngMenus = sysMenus.Where(f => f.ParentId == 0).OrderBy(f => f.ShowOrder).Select(f => new NgMenu()
            {
                Group = true,
                key = f.Id,
                Text = f.Text,
                Icon = f.Icon
            }
            ).ToList();
            ngMenus.ForEach(f =>
            {
                GetLevelMenus(sysMenus, f);
            });
            ngMenus.RemoveAll(f => f.Group && (f.Children == null || f.Children.Count == 0));

            return ngMenus;
        }

        private void GetLevelMenus(List<SysMenu> sysMenus, NgMenu ngMenu)
        {
            var acls = SysObjectAclService.QueryAclNames();
            List<NgMenu> ngMenus = sysMenus.Where(f => f.ParentId == ngMenu.key && (CurrentUser.Account == "admin" || acls.Contains(f.AclCode)))
                .OrderBy(f => f.ShowOrder).Select(f =>
                {
                    var menu = new NgMenu()
                    {
                        key = f.Id,
                        Text = f.Text,
                        Icon = f.Icon
                    };
                    if (f.IsExternalLink) { menu.ExternalLink = f.Link; }
                    else { menu.Link = f.Link; }
                    return menu;
                }).ToList();

            ngMenus.ForEach(f =>
            {
                GetLevelMenus(sysMenus, f);
            });
            ngMenu.Children = ngMenus;
            if (ngMenu.Children.Count > 0)
            {
                ngMenu.Group = true;
            }
        }

        #endregion

        /// <summary>
        /// 获取菜单树 排除某个菜单及其子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<NgMenu> GetMenusExcept(int id)
        {
            var data = GetMenus();
            var menus = GetMenusWithoutId(data.CloneJson(), id);
            return menus;
        }


        private List<NgMenu> GetMenusWithoutId(List<NgMenu> menus, int id)
        {
            var result = menus.Where(f => f.key != id).ToList();
            foreach (var menu in result)
            {
                if (menu.Children != null && menu.Children.Count > 0)
                {
                    menu.Children = GetMenusWithoutId(menu.Children, id);
                }
            }
            return result;
        }


        #region

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public new QueryResult<SysMenuDto> Query(SysMenuQueryParam queryParam)
        {
            QueryResult<SysMenuDto> result = new QueryResult<SysMenuDto>();
            var r = SysMenuRepository.Query(queryParam);
            result.List = QueryLevelMenus(r.List);
            result.Total = r.Total;
            return result;
        }

        /// <summary>
        /// 组装菜单层级数据
        /// </summary>
        /// <param name="sysMenus"></param>
        private List<SysMenuDto> QueryLevelMenus(List<SysMenu> sysMenus)
        {
            List<SysMenuDto> menuDtos = sysMenus.Where(f => f.ParentId == 0).Select(f =>
            {
                var m = new SysMenuDto() { Group = true, Key = f.Id };
                m.CopyFrom(f);
                return m;
            }
            ).OrderBy(f => f.ShowOrder).ToList();
            menuDtos.ForEach(f =>
            {
                QueryLevelMenus(sysMenus, f);
            });

            return menuDtos;
        }

        private void QueryLevelMenus(List<SysMenu> sysMenus, SysMenuDto menuDto)
        {
            var order = 1;
            List<SysMenuDto> menuDtos = sysMenus.Where(f => f.ParentId == menuDto.Key).OrderBy(f => f.ShowOrder).Select(f =>
            {
                var m = new SysMenuDto() { Group = true, Key = f.Id, Order = order++ };
                m.CopyFrom(f);
                return m;
            }).ToList();

            menuDtos.ForEach(f =>
            {
                QueryLevelMenus(sysMenus, f);
            });
            menuDto.Children = menuDtos;
        }


        #endregion


        public override ResponseResult Update(SysMenu menu)
        {
            Result = base.Update(menu);
            if (Result.Code == ResponseStatusCode.OK)
            {
                Cache.Remove(CacheKeys.SysMenu.Format(CurrentUser.Id));
            }

            return Result;
        }

        public override ResponseResult Delete(int id)
        {
            Result = base.Delete(id);
            if (Result.Code == ResponseStatusCode.OK)
            {
                Cache.Remove(CacheKeys.SysMenu.Format(CurrentUser.Id));
            }

            return Result;
        }

        public override ResponseResult Add(SysMenu menu)
        {
            Result = base.Add(menu);
            if (Result.Code == ResponseStatusCode.OK)
            {
                Cache.Remove(CacheKeys.SysMenu.Format(CurrentUser.Id));
            }

            return Result;
        }

    }
}
