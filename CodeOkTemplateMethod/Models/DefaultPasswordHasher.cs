using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkTemplateMethod.Models
{
    public class DefaultPasswordHasher
    {
        public virtual string GetHash(string password)
        {
            return password.GetHashCode().ToString();
        }
    }
}
