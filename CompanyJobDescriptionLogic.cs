using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository) { }
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
        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos)
            {
                if (string.IsNullOrEmpty(poco_item.JobName))
                {
                    errors.Add(new ValidationException(300, "JobName cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco_item.JobDescriptions)) 
                {
                    errors.Add(new ValidationException (301, "JobDescriptions cannot be empty"));
                }
                
            }
            if (errors.Any()) {
                throw new AggregateException(errors);
            }
        }
    }
}
