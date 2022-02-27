using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class VID:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (getDigits(int.Parse(value.ToString()), 0) == 14)
                return ValidationResult.Success;

            return new ValidationResult("Must be 14 Number");

        }
        public static int getDigits(int Number, int NumberOfdigits)
        {
            if (Number == 0)
                return NumberOfdigits;

            return getDigits(Number / 10, ++NumberOfdigits);
        }
    }
}