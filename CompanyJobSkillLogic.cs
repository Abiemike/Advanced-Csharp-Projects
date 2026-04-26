using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic:BaseLogic<CompanyJobSkillPoco>
    {
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository):base(repository) 
        {
            
        }
        public override void Add(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            List<ValidationException> errors= new List<ValidationException>();
            foreach (var poco_item in pocos) 
            {
                if (poco_item != null && poco_item.Importance < 0)
                {
                    errors.Add(new ValidationException(400, "Importance cannot be less than 0"));
                }
            }
            if (errors.Count > 0)
            {
            throw new AggregateException(errors);
            }
        }
    }
}
