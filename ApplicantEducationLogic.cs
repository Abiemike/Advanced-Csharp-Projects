using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        // Constructor that accepts IDataRepository<ApplicantEducationPoco> and passes it to the base constructor
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository)
            : base(repository)
        {
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
           
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        // You can add any specific logic related to ApplicantEducationPoco here
        // Override the Verify method to implement rule validation





        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            // List to collect validation exceptions
            List<ValidationException> validationExceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                // Validate "Major" property rule
                if (string.IsNullOrEmpty(poco.Major) || poco.Major.Length < 3)
                {
                    validationExceptions.Add(new ValidationException(107, "Major cannot be empty or less than 3 characters."));
                }

                if (poco.StartDate.HasValue &&poco.StartDate.Value > DateTime.Today)
                {
                    validationExceptions.Add(new ValidationException(108, "Cannot be greater than today"));
                }
                if (poco.CompletionDate.HasValue && poco.StartDate.HasValue && poco.CompletionDate.Value < poco.StartDate.Value)
                {
                    validationExceptions.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate."));
                }
            }

            // If there are any validation exceptions, throw an AggregateException
            if (validationExceptions.Any())
            {
                throw new AggregateException(validationExceptions);
            }
        }

    }
}
