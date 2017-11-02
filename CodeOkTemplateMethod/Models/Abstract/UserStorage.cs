using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkTemplateMethod.Models
{
    public abstract class UserStorage
    {
        public abstract QueryResult Save(User user);

        public abstract User FindUserByLogin(string username, string passwordHash);
    }
}
