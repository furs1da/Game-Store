using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Models.ValidationAttributes
{
    public class NicknameUniqueAttribute : ValidationAttribute
    {
        private readonly string _IdPropertyName;
        public NicknameUniqueAttribute(string IdPropertyName)
        {
            _IdPropertyName = IdPropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string name = value.ToString();
            var property = validationContext.ObjectType.GetProperty(_IdPropertyName);
            if (property != null)
            {
                var idValue = property.GetValue(validationContext.ObjectInstance, null);
                var _context = (StoreContext)validationContext.GetService(typeof(StoreContext));

                var checkForUpdate = _context.Users.SingleOrDefault(e => e.UserName == value.ToString() && e.Id == idValue);
                
                if(checkForUpdate != null)
                    return ValidationResult.Success;

                var entity = _context.Users.SingleOrDefault(e => e.UserName == value.ToString() && e.Id != idValue);

                if (entity != null)
                {
                    return new ValidationResult(GetErrorMessage(value.ToString()));
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string name)
        {
            return $"Nickname {name} is already in use.";
        }
    }
}

