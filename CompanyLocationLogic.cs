using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic:BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository):base(repository) 
        {
            
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
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos)
            {  //CompanyLocationLogic CountryCode CountryCode cannot be empty 500
                
                    if (string.IsNullOrEmpty(poco_item.CountryCode))
                    {
                        errors.Add(new ValidationException(500, "CountryCode cannot be empty"));
                    }
                    //CompanyLocationLogic Province Province cannot be empty 501
                    if (string.IsNullOrEmpty(poco_item.Province))
                    {
                        errors.Add(new ValidationException(501, "Province cannot be empty"));
                    }
                    //CompanyLocationLogic Street Street cannot be empty 502
                    if (string.IsNullOrEmpty(poco_item.Street))
                    {
                        errors.Add(new ValidationException(502, "Street cannot be empty"));
                    }
                    //CompanyLocationLogic City City cannot be empty 503
                    if (string.IsNullOrEmpty(poco_item.City))
                    {
                        errors.Add(new ValidationException(503, "City cannot be empty"));
                    }
                //CompanyLocationLogic PostalCode PostalCode cannot be empty 504
                if (string.IsNullOrEmpty(poco_item.PostalCode))
                {
                    errors.Add(new ValidationException(504, "PostalCode cannot be empty"));
                }
            }
            if (errors.Count > 0) 
            {
            throw new AggregateException(errors);
            }
        }
    }
}
