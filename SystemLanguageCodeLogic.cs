using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        private readonly IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public  void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);

            _repository.Add(pocos);
        }

        public  void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }
        protected  void Verify(SystemLanguageCodePoco[] pocos)
        {

            List<ValidationException> errors = new List<ValidationException>();

            foreach (var poco_item in pocos)
            {
                // SystemLanguageCodeLogic LanguageID - LanguageID cannot be empty 1000
                if (string.IsNullOrEmpty(poco_item.LanguageID))
                {
                    errors.Add(new ValidationException(1000, "LanguageID cannot be empty"));
                }

                // SystemLanguageCodeLogic Name - Name cannot be empty 1001
                if (string.IsNullOrEmpty(poco_item.Name))
                {
                    errors.Add(new ValidationException(1001, "Name cannot be empty"));
                }

                // SystemLanguageCodeLogic NativeName - NativeName cannot be empty 1002
                if (string.IsNullOrEmpty(poco_item.NativeName))
                {
                    errors.Add(new ValidationException(1002, "NativeName cannot be empty"));
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
