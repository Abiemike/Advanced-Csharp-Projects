using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic:BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository):base(repository) 
        {
        
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
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos)
            {
                if (string.IsNullOrEmpty(poco_item.CompanyName)|| poco_item.CompanyName.Length <= 2)
                {
                    errors.Add(new ValidationException(106, "CompanyName must be greater than 2 characters"));
                }
                if (string.IsNullOrEmpty(poco_item.CompanyDescription)|| poco_item.CompanyDescription.Length <= 2)
                {

                    errors.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));
                }
            }
            if (errors.Count>0) {
            throw new AggregateException(errors);
            }
        }
    }
}
