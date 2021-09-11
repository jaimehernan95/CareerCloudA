using System;
using System.Collections.Generic;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyDescription))
                {
                    exceptions.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));
                }
                else if (poco.CompanyDescription.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));
                }

                if (string.IsNullOrEmpty(poco.CompanyName))
                {
                    exceptions.Add(new ValidationException(106, "CompanyDescription must be greater than 2 characters"));
                }
                else if (poco.CompanyName.Length < 3)
                {
                    exceptions.Add(new ValidationException(106, "CompanyDescription must be greater than 2 characters"));
                }
            }

            if (exceptions.Count > 0)
            {

                throw new AggregateException(exceptions);
            }

        }
        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}