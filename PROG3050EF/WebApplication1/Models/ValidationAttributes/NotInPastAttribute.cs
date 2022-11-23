using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Models.ValidationAttributes
{
    public class NotInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
           

            if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                if (date >= DateTime.Now)
                {
                    return ValidationResult.Success;
                }

            }

            string msg = base.ErrorMessage ?? $"Date must be in the future.";
            return new ValidationResult(msg);
        }
    }
}
