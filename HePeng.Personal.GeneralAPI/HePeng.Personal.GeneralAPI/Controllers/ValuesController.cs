using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Jwt;
using HePeng.Personal.Common.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// API示例
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    //[Authorize(Roles = JwtConsts.RoleName)]
    //[Authorize(Roles = "Admin")]//or [Authorize(Policy="RequireAdmin")]
    public class ValuesController : BaseController
    {
        IConfiguration Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// get strs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", Configuration["Logging:LogLevel:Default"] };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
