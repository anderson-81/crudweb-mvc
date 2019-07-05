using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudweb_mvc.ViewModels
{
    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if(value != null)
            {
                if(DateTime.Parse(value.ToString()) <= DateTime.Now.AddYears(-18))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Allowed only to register individuals over 18 years..");
                }
            }
            else
            {
                return new ValidationResult("Date of Birth is Empty.");
            }
        }
    }
}