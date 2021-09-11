using System;
using System.Collections.Generic;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;


namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyJobDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.JobDescriptions))
                {
                    exceptions.Add(new ValidationException(301, "JobDescription cant be empty"));
                }
               

                if (string.IsNullOrEmpty(poco.JobName))
                {
                    exceptions.Add(new ValidationException(300, "JobName mcannot be empty"));
                }
              
            }

            if (exceptions.Count > 0)
            {

                throw new AggregateException(exceptions);
            }

        }
        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}