using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkTemplateMethod.Models
{
    public class UserManager : BaseUserManager<User>
    {
        DefaultValidator PasswordValidator { get; set; } = new DefaultValidator() { AllowNonAlphanumeric = false, RequireLength = true, RequiredLength = 10 };
        DefaultValidator UsernameValidator { get; set; } = new DefaultValidator() { AllowNonAlphanumeric = false, RequireUppercase = true, RequireLength = true, RequiredLength = 5 };
        DefaultPasswordHasher PasswordHasher { get; set; } = new DefaultPasswordHasher();
        UserStorage UserStorage { get; set; } = new FileUserStorage(ConfigurationManager.AppSettings["UserStorage"]);


        protected override User FindUserByLogin(User user)
        {
            return UserStorage.FindUserByLogin(user.Username, user.PasswordHash);
        }

        protected override string HashPassword(string password)
        {
            return PasswordHasher.GetHash(password);
        }

        protected override QueryResult SaveUser(User user)
        {
            return UserStorage.Save(user);
        }

        protected override QueryResult ValidateUser(User user, string password)
        {
            List<string> errors = new List<string>();
            bool isValid = UsernameValidator.IsValid(user.Username);
            if (!isValid)
                errors.Add("Имя пользователя должно состоять не менее чем из 10 символов, состоять только из латинских букв и цифр, а так же включать буквы в верхнем регистре.");

            if (!PasswordValidator.IsValid(password))
            {
                isValid &= false;
                errors.Add("Пароль должен состоять не менее чем из 10 символов, а так же состоять только из латинских букв и цифр.");
            }

            return new QueryResult(isValid, errors);
        }
    }
}
