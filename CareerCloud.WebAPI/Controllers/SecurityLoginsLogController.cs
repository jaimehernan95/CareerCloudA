using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("loginslog/{id}")]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            SecurityLoginsLogPoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("loginslog")]
        public ActionResult GetAllSecurityLoginLog()
        {
            var loginLogs = _logic.GetAll();
            if (loginLogs == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(loginLogs);
            }

        }

        [HttpPost]
        [Route("loginslog")]
        public ActionResult PostSecurityLoginLog(
            [FromBody] SecurityLoginsLogPoco[] secLoginlogPocos)
        {
            _logic.Add(secLoginlogPocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginslog")]
        public ActionResult PutSecurityLoginLog(
            [FromBody] SecurityLoginsLogPoco[] secLoginlogPocos)
        {
            _logic.Update(secLoginlogPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginslog")]
        public ActionResult DeleteSecurityLoginLog(
            [FromBody] SecurityLoginsLogPoco[] secLoginlogPocos)
        {
            _logic.Delete(secLoginlogPocos);
            return Ok();
        }



    }
}