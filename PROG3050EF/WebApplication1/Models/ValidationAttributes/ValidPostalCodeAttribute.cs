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
    public class ValidPostalCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            string postalCodeRegex = @"^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";

            if (value is string)
            {
                string postalCode = (string)value;
                if ((Regex.Match(postalCode, postalCodeRegex).Success))
                {
                    return ValidationResult.Success;
                }

            }

            string msg = base.ErrorMessage ?? $"Postal Code is invalid.";
            return new ValidationResult(msg);
        }
    }
}
