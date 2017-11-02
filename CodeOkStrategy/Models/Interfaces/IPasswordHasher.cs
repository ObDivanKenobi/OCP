using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkStrategy.Models
{
    public interface IPasswordHasher
    {
        string GetHash(string password);
    }
}
