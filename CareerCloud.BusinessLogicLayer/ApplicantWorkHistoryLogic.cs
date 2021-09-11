using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantWorkHistoryPoco poco in pocos)
            {
                if (poco.CompanyName.Length < 3)
                {
                    exceptions.Add(new ValidationException(105, "Must be greater then 2 characters"));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}