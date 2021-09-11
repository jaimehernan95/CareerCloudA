using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantEducationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, "Major is balnk please fix it"));

                }
                else if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, "Major'lenght is less than 3 character ..please fix it"));
                }
                if (poco.StartDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, "your time is wrong"));

                }
                if (poco.CompletionDate < poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate"));

                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}