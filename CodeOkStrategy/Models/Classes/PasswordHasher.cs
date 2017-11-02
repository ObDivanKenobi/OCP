﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkStrategy.Models
{
    public class DefaultPasswordHasher : IPasswordHasher
    {
        public string GetHash(string password)
        {
            return password.GetHashCode().ToString();
        }
    }
}
