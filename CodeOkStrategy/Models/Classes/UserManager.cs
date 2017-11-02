using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkStrategy.Models
{
    public class UserManager : IUserManager<User>
    {
        public IList<IValidator<string>> PasswordValidators { get; set; } = new List<IValidator<string>>
            {
                new NotEmpty("Пароль не может быть пустой строкой"), new AlphanumericOnly("В пароле допустимы только латинские буквы и цифры"),
                new RequireMinLength(5, "Минимальная длина пароля - 5 символов"), new RestrictMaxLength(20, "Максимальная длина пароля - 20 символов"),
                new RequireLowercase("Пароль должен содержать хотя бы одну заглавную букву"), new RequireUppercase("Пароль должен содержать хотя бы одну строчную букву"),
                new RequireNumeric("Пароль должен содержать хотя бы одну цифру")
            };
        public IList<IValidator<string>> UsernameValidators { get; set; } = new List<IValidator<string>>
            {
                new NotEmpty("Имя пользователя не может быть пустой строкой"), new AlphanumericOnly("В имени пользователя допустимы только латинские буквы и цифры"),
                new RequireMinLength(5, "Минимальная длина имени пользователя - 5 символов"),
                new RequireLowercase("Имя пользователя должно содержать хотя бы одну заглавную букву"),
                new RequireUppercase("Имя пользователя должно содержать хотя бы одну строчную букву")
            };

        public IPasswordHasher PasswordHasher { get; set; } = new DefaultPasswordHasher();
        public IStorage<User> UserStorage { get; set; } = new FileUserStorage(ConfigurationManager.AppSettings["UserStorage"]);

        public bool Authenticate(User user, string password)
        {
            user.PasswordHash = PasswordHasher.GetHash(password);
            return UserStorage.FindByLogin(user.Username, user.PasswordHash) != null;
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
            bool isValid = true;

            foreach (var validator in UsernameValidators)
            {
                if (!validator.IsValid(user.Username))
                {
                    isValid &= false;
                    errors.Add(validator.ErrorMessage);
                }
            }

            foreach (var validator in PasswordValidators)
            {
                if (!validator.IsValid(password))
                {
                    isValid &= false;
                    errors.Add(validator.ErrorMessage);
                }
            }

            return new QueryResult(isValid, errors);
        }
    }
}
