using crudweb_mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace crudweb_mvc.ViewModels
{
    public class CheckEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            try
            {
                if (value != null)
                {
                    if (Regex.IsMatch(value.ToString(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    {
                        string email = value.ToString();
                        if (new dbRegistrationContext().PERSON.Where(p => p.EMAIL == email).ToList().Count() > 0)
                        {
                            return new ValidationResult("E-mail already registered.");
                        }
                    }
                    else
                    {
                        return new ValidationResult("Invalid E-mail.");
                    }
                }
                else
                {
                    return new ValidationResult("E-mail is empty.");
                }

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Error checking E-mail.");
            }
        }
    }
}