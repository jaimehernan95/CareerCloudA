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
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        [HttpGet]
        [Route("login/{securityLoginId}")]
        public ActionResult GetSecurityLogin(Guid id)
        {
            SecurityLoginPoco poco = _logic.Get(id);
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
        [Route("login")]
        public ActionResult GetAllSecurityLogin()
        {
            var logins = _logic.GetAll();
            if (logins == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(logins);
            }

        }

        [HttpPost]
        [Route("login")]
        public ActionResult PostSecurityLogin(
            [FromBody] SecurityLoginPoco[] secLogPocos)
        {
            _logic.Add(secLogPocos);
            return Ok();
        }

        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin(
            [FromBody] SecurityLoginPoco[] secLogPocos)
        {
            _logic.Update(secLogPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin(
            [FromBody] SecurityLoginPoco[] secLogPocos)
        {
            _logic.Delete(secLogPocos);
            return Ok();
        }


    }



}