using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GameStore.Models;
using GameStore.Data;
using System.Text.RegularExpressions;

namespace GameStore.Models.ValidationAttributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
         
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            string passwordRegex = @"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

            if (value is string)
            {
                string password = (string)value;
                if ((Regex.Match(password, passwordRegex).Success))
                {
                    return ValidationResult.Success;
                }

            }

            string msg = base.ErrorMessage ?? $"Please, ensure your password has at least one lowercase letter, one uppercase letter, one digit, one special character, and is at least eight characters long";
            return new ValidationResult(msg);
        }
    }
}
