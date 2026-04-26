using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
    {
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository)
        {

        }
        public override void Add(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantResumePoco[] pocos)
        {
            List<ValidationException> validationExceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Resume))
                {
                    validationExceptions.Add(new ValidationException(Code: 113, message: "Resume cannot be empty"));
                }
            }

            if (validationExceptions.Count > 0) 
            {
                throw new AggregateException(validationExceptions);
            }

        }
    }
}
