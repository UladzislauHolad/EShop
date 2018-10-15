﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Infrastructure
{
    public static class ModelStateTransformator
    {
        public static List<string> ToErrorsStringArray(this ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();

            foreach (var error in modelState.Values)
            {
                errors.AddRange(error.Errors.Select(e => e.ErrorMessage));
            }

            return errors;
        }
    }
}
