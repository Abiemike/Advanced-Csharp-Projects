using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic:BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository< ApplicantProfilePoco> repository):base(repository)
        {
            
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            var validateException = new List<ValidationException>();
            foreach (var poco in pocos) 
            {
                if (poco.CurrentSalary < 0) 
                {
                    validateException.Add(new ValidationException(111, "CurrentSalary cannot be negative"));
                }
                if (poco.CurrentRate < 0)
                {
                    validateException.Add(new ValidationException(112, "CurrentRate cannot be negative"));
                }
            }
            if (validateException.Count > 0) ; 
            {
            throw new AggregateException(validateException);
            }
        }


       
    }
}
