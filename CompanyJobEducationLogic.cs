using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository) { }
        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos) 
            {
                if (string.IsNullOrEmpty(poco_item.Major)||poco_item.Major.Length<2) 
                {
                    //Importance cannot be less than 0
                    errors.Add(new ValidationException(200, "Major must be at least 2 characters"));
                }
                if (poco_item.Importance < 0)
                {
                    //Importance cannot be less than 0
                    errors.Add(new ValidationException(201, "Importance cannot be less than 0"));
                }


            }

            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }
     }

    }
    
}
