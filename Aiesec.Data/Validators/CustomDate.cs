using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aiesec.Data.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDate:Attribute, IModelValidator, IClientModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (Convert.ToDateTime(context.Model) > DateTime.Now)
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", "This date can't be in the future")
                };
            else if (Convert.ToDateTime(context.Model) < new DateTime(1950, 1, 1))
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", "This date should not be before 1950")
                };
            else
                return Enumerable.Empty<ModelValidationResult>();
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNull : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (Convert.ToInt32(context.Model) == 0)
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", "This field must have a value")
                };
            else
                return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
