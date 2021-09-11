using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;
namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                if (poco.ApplicationDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(110, "ApplicationDate cannot be greater than today "));
                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}