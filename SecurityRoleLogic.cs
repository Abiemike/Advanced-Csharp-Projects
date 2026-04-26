using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic:BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository):base(repository) 
        {
            
        }
        public override void Add(SecurityRolePoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();

            foreach (var poco_item in pocos)
            {
                // SecurityRoleLogic Role - Role cannot be empty 800
                if (string.IsNullOrEmpty(poco_item.Role))
                {
                    errors.Add(new ValidationException(800, "Role cannot be empty"));
                }
            }

            // If there are any validation errors, throw an AggregateException
            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }
        }
    }
}
