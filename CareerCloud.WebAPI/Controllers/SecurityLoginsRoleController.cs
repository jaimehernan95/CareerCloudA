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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);

        }

        [HttpGet]
        [Route("loginsrole/{id}")]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            SecurityLoginsRolePoco poco = _logic.Get(id);
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
        [Route("loginsrole")]
        public ActionResult GetAllSecurityLoginRole()
        {
            var roles = _logic.GetAll();
            if (roles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(roles);
            }

        }

        [HttpPost]
        [Route("loginsrole")]
        public ActionResult PostSecurityLoginRole(
            [FromBody] SecurityLoginsRolePoco[] secLogRolePocos)
        {
            _logic.Add(secLogRolePocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginsrole")]
        public ActionResult PutSecurityLoginRole(
            [FromBody] SecurityLoginsRolePoco[] secLogRolePocos)
        {
            _logic.Update(secLogRolePocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginsrole")]
        public ActionResult DeleteSecurityLoginRole(
            [FromBody] SecurityLoginsRolePoco[] secLogRolePocos)
        {
            _logic.Delete(secLogRolePocos);
            return Ok();
        }
    }
}