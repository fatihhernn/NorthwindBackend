using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public class ValidationTools
    {
        //productManagerda yazdığım if li kısmı alıp, burada merkezileştircem
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);            
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
