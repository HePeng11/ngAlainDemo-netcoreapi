using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.Dtos;
using HePeng.Personal.GeneralAPI.Other;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// user
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.users_CUD, "other")]
    public class FileController : BaseController
    {
        ISysFileService SysFileService { get; }

        ISysFileRelationService SysFileRelationService { get; }
        IHostingEnvironment Env { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="sysFileService"></param>
        /// <param name="sysFileRelationService"></param>
        public FileController(IHostingEnvironment env, ISysFileService sysFileService, ISysFileRelationService sysFileRelationService)
        {
            Env = env;
            SysFileService = sysFileService;
            SysFileRelationService = sysFileRelationService;
        }

        /// <summary>
        /// 上传文件 仅上传，确认时需要调用SaveRelation接口
        /// </summary>
        /// <param name="formData">表单数据</param>
        /// <param name="uploadType">上传类型</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public object Upload([FromForm]IFormCollection formData, [FromQuery]UploadType uploadType)
        {
            if (uploadType == UploadType.UserPicture)
            {
                if (formData.Files.Count != 1)
                {
                    return new { };
                }
                var file = formData.Files[0];
                var extName = file.FileName.GetFileExtName();
                string targetPath = "/uploads/" + Guid.NewGuid().ToString().Replace("-", "") + extName;
                FileInfo fileInfo = new FileInfo(Env.WebRootPath + targetPath);
                using (FileStream fs = new FileStream(fileInfo.ToString(), FileMode.Create))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                var entity = new SysFile() { Name = file.FileName, CreateTime = DateTime.Now, ExtName = fileInfo.Extension, Path = targetPath, UploadType = uploadType };
                SysFileService.Add(entity);
                return new { fileId = entity.Id, url = UrlPrefix + targetPath };
            }

            return new { };
        }

        /// <summary>
        /// 保存文件与业务数据关系
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpPost("saveRelation/{fileId}")]
        public object SaveRelation(int fileId, [FromQuery]int businessId)
        {
            return SysFileRelationService.Add(new SysFileRelation() { BusinessId = businessId, FileId = fileId });
        }

    }
}