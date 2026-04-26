using System;
using System.Collections.Generic;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic : BaseLogic<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
            : base(repository)
        {
        }
        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);  // Example assuming CountryCode is the identifier
        }
        public override void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);  // Perform validation
            base.Update(pocos);  // Proceed with the update
        }
        public override void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);  // Perform validation
            base.Add(pocos);  // Proceed with the update
        }
        protected override void Verify(SystemCountryCodePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                // Rule for Code
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, "Code cannot be empty."));
                }

                // Rule for Name
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, "Name cannot be empty."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
