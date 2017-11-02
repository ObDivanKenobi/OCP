﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkStrategy.Models
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; }
        bool IsValid(T dataToValidate);
    }
}
