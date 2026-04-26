using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {

        }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        //ApplicantSkillLogic StartMonth Cannot be greater than 12 101
        //ApplicantSkillLogic EndMonth Cannot be greater than 12 102
        //ApplicantSkillLogic StartYear Cannot be less then 1900 103
        //ApplicantSkillLogic EndYear Cannot be less then StartYear 104
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (var poco_item in pocos)
            {
                if (poco_item.StartMonth > 12)
                {
                    errors.Add(new ValidationException(101, "StartMonth Cannot be greater than 12"));
                }
                if (poco_item.EndMonth > 12)
                {
                    errors.Add(new ValidationException(102, "EndMonth Cannot be greater than 12"));
                }
                if (poco_item.StartYear < 1900)
                {
                    errors.Add(new ValidationException(103, "StartYear Cannot be less then 1900"));
                }
                if (poco_item.EndYear<poco_item.StartYear) 
                {
                    errors.Add(new ValidationException(104, "EndYear Cannot be less then StartYear"));
                }

            }
            if (errors.Any()) 
            {
                throw new AggregateException(errors);
            }
        }
    }
}