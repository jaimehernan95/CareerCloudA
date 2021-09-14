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
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }
        [HttpGet]
        [Route("SystemLanguage/{code}")]
        public ActionResult GetSystemLanguageCode(String languageId)
        {
            SystemLanguageCodePoco poco = _logic.Get(languageId);
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
        [Route("SystemLanguage")]
        public ActionResult GetAllSystemLanguageCode()
        {
            var languages = _logic.GetAll();
            if (languages == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(languages);
            }

        }

        [HttpPost]
        [Route("SystemLanguage")]
        public ActionResult PostSystemLanguageCode(
            [FromBody] SystemLanguageCodePoco[] sysLangPocos)
        {
            _logic.Add(sysLangPocos);
            return Ok();
        }

        [HttpPut]
        [Route("SystemLanguage")]
        public ActionResult PutSystemLanguageCode(
            [FromBody] SystemLanguageCodePoco[] sysLangPocos)
        {
            _logic.Update(sysLangPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("SystemLanguage")]
        public ActionResult DeleteSystemLanguageCode(
            [FromBody] SystemLanguageCodePoco[] sysLangPocos)
        {
            _logic.Delete(sysLangPocos);
            return Ok();
        }
    }
}