using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                if (poco.CurrentSalary < 0)
                {
                    exceptions.Add(new ValidationException(111, "CurrentSalary cannot be negative"));
                }
                if (poco.CurrentRate < 0)
                {
                    exceptions.Add(new ValidationException(112, "CurrentRate cannot be negative"));
                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}