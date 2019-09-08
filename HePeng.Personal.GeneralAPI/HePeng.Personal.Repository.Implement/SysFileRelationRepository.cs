using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 附件、文件从属关系仓储层接口
    /// </summary>
    public class SysFileRelationRepository : BaseRepository<SysFileRelation, QueryParam>, ISysFileRelationRepository
    {
        public List<SysFile> GetFile(List<int> businessIds, UploadType uploadType)
        {
            return SqlSugarClient.Queryable<SysFile, SysFileRelation>((f, r) => new object[] { JoinType.Inner, f.Id == r.FileId })
                 .Where((f, r) => f.Status == Status.正常 && r.Status == Status.正常 && f.UploadType == uploadType && businessIds.Contains(r.BusinessId))
                 .Select((f, r) => f).ToList();
        }

        public override SysFileRelation Add(SysFileRelation t)
        {
            var result = SqlSugarClient.Ado.UseTran(() =>
              {
                  if (t.UploadType == UploadType.UserPicture)
                  {
                      SqlSugarClient.Updateable<SysFileRelation>().UpdateColumns(f => new SysFileRelation { Status = Status.删除 })
                      .Where(f => f.BusinessId == t.BusinessId).ExecuteCommand();
                  }
                  var r = base.Add(t);
                  return r;
              });
            if (!result.IsSuccess)
            {
                throw result.ErrorException;
            }
            return result.Data;
        }
    }
}
