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
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeController()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet]
        [Route("CountryCode/{code}")]
        public ActionResult GetSystemCountryCode(String code)
        {
            SystemCountryCodePoco poco = _logic.Get(code);
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
        [Route("CountryCode")]
        public ActionResult GetAllSystemCountryCode()
        {
            var countries = _logic.GetAll();
            if (countries == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(countries);
            }

        }

        [HttpPost]
        [Route("CountryCode")]
        public ActionResult PostSystemCountryCode(
            [FromBody] SystemCountryCodePoco[] sysCountPocos)
        {
            _logic.Add(sysCountPocos);
            return Ok();
        }

        [HttpPut]
        [Route("CountryCode")]
        public ActionResult PutSystemCountryCode(
            [FromBody] SystemCountryCodePoco[] sysCountPocos)
        {
            _logic.Update(sysCountPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("CountryCode")]
        public ActionResult DeleteSystemCountryCode(
            [FromBody] SystemCountryCodePoco[] sysCountPocos)
        {
            _logic.Delete(sysCountPocos);
            return Ok();
        }
    }
}