using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkStrategy.Models
{
    public interface IUserManager<TUser> where TUser : User
    {
        IList<IValidator<string>> PasswordValidators { get; set; }
        IList<IValidator<string>> UsernameValidators { get; set; }
        IPasswordHasher PasswordHasher { get; set; }
        IStorage<TUser> UserStorage { get; set; }

        bool Authenticate(User user, string password);
        QueryResult AddUser(TUser user, string password);
    }
}
