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
    [Route("api / careercloud / applicant / v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;
        public ApplicantJobApplicationController()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }


        [HttpGet]
        [Route("JobApplication/{id}")]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            ApplicantJobApplicationPoco poco = _logic.Get(id);
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
        [Route("JobApplication")]
        public ActionResult GetAllApplicantJobApplication()
        {
            var applicants = _logic.GetAll();

            if (applicants == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(applicants);
            }

        }

        [HttpPost]
        [Route("JobApplication")]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] appEdupocos)
        {
            _logic.Add(appEdupocos);
            return Ok();

        }

        [HttpPut]
        [Route("JobApplication")]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] appEdupocos)
        {
            _logic.Update(appEdupocos);
            return Ok();

        }

        [HttpDelete]
        [Route("JobApplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] appEdupocos)
        {
            _logic.Delete(appEdupocos);
            return Ok();

        }

    }
}