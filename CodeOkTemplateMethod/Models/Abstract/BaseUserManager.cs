using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkTemplateMethod.Models
{
    public abstract class BaseUserManager<TUser> where TUser : User
    {
        public bool Authenticate(TUser user, string password)
        {
            if (!ValidateUser(user, password).Succeed)
                return false;

            user.PasswordHash = HashPassword(password);
            return FindUserByLogin(user) != null;
        }

        public QueryResult AddUser(TUser user, string password)
        {
            QueryResult validationResult = ValidateUser(user, password);

            if (!validationResult.Succeed)
                return validationResult;

            user.PasswordHash = HashPassword(password);

            return SaveUser(user);
        }

        protected abstract TUser FindUserByLogin(TUser user);
        protected abstract QueryResult ValidateUser(TUser user, string password);
        protected abstract QueryResult SaveUser(TUser user);
        protected abstract string HashPassword(string password);
    }
}
