using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Models
{
    public class UserManager
    {
        Validator PasswordValidator { get; set; } = new Validator() { AllowNonAlphanumeric = false, RequireLength = true, RequiredLength = 10 };
        Validator UsernameValidator { get; set; } = new Validator() { AllowNonAlphanumeric = false, RequireUppercase = true, RequireLength = true, RequiredLength = 5 };
        PasswordHasher PasswordHasher { get; set; } = new PasswordHasher();
        UserStorage UserStorage { get; set; } = new UserStorage(ConfigurationManager.AppSettings["UserStorage"]);

        public bool Authenticate(User user, string password)
        {
            if(!ValidateUser(user, password).Succeed)
                return false;

            return UserStorage.FindUserByLogin(user.Username, user.PasswordHash) != null;
        }

        public QueryResult AddUser(User user, string password)
        {
            QueryResult validationResult = ValidateUser(user, password);
            if (!validationResult.Succeed)
                return validationResult;

            user.PasswordHash = PasswordHasher.GetHash(password);
            return UserStorage.Save(user);
        }
        
        QueryResult ValidateUser(User user, string password)
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
