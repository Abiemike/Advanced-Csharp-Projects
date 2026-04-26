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
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }
        //ApplicantWorkHistoryLogic CompanyName Must be greater then 2 characters 105

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos)
            {
                if (string.IsNullOrEmpty(poco_item.CompanyName)||  poco_item.CompanyName.Length <=2)
                {
                    errors.Add(new ValidationException(105, "CompanyName Must be greater then 2 characters"));

                }
            }
            if (errors.Count > 0) 
            {
                throw new AggregateException(errors);          
            }
        }
    }
}