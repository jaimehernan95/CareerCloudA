using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
        {
        }
        protected override void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SecurityRolePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Role))
                {
                    exceptions.Add(new ValidationException(800, "Role Cannot be empty"));
                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

        }
        public override void Add(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

    }
}