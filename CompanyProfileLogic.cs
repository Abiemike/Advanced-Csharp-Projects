using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository):base(repository) 
        {
            
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();

            foreach (var poco_item in pocos)
            {
                // CompanyProfileLogic CompanyWebsite - Valid websites must end with the following extensions – ".ca", ".com", ".biz" 600
                if (string.IsNullOrEmpty(poco_item.CompanyWebsite) ||
                    (!poco_item.CompanyWebsite.EndsWith(".ca") ||
                     !poco_item.CompanyWebsite.EndsWith(".com") ||
                     !poco_item.CompanyWebsite.EndsWith(".biz")))
                {
                    errors.Add(new ValidationException(600, "Valid websites must end with the following extensions: .ca, .com, .biz"));
                }

                // CompanyProfileLogic ContactPhone - Must correspond to a valid phone number (e.g. 416-555-1234) 601
                if (string.IsNullOrEmpty(poco_item.ContactPhone))
                {
                    errors.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                }
                else
                {
                    // Split the phone number by '-'
                    string[] phoneComponents = poco_item.ContactPhone.Split('-');

                    // Check if the phone number contains exactly three components (XXX-XXX-XXXX)
                    if (phoneComponents.Length != 3)
                    {
                        errors.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                    }
                    else
                    {
                        // Validate the length of each component
                        if (phoneComponents[0].Length != 3 || !phoneComponents[0].All(char.IsDigit))
                        {
                            errors.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                        }
                        else if (phoneComponents[1].Length != 3 || !phoneComponents[1].All(char.IsDigit))
                        {
                            errors.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                        }
                        else if (phoneComponents[2].Length != 4 || !phoneComponents[2].All(char.IsDigit))
                        {
                            errors.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                        }
                    }
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
