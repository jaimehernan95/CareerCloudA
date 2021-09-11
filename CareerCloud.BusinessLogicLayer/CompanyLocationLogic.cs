using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, "CountryCode cannot be empty "));
                }
                if (string.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501, "Province cannot be empty "));
                }
                if (string.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502, "Street cannot be empty "));
                }
                if (string.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503, "City cannot be empty "));
                }

                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, "PostalCode cannot be empty "));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}