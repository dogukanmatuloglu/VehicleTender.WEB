﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleTender.API.Validation.Validators.Base;

namespace VehicleTender.API.Validation.Validators.Concrete
{
    public record DefaultValidator<T>() : IValidator<T>
    {
        public List<(bool, Exception)> Validate(T value, int? value2, int? value3, string value4, PropertyInfo? info, object? model)
        {
            return new List<(bool, Exception)>();
        }
    }
}
