namespace MyCoolWebServer.GameStoreApplication.Utilities
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = "Password should be at least 6 symbols long, should have at least 1 upper case letter, should have at least 1 lower case letter and at least 1 digit.";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;
            if(password == null)
            {
                return true;
            }

            return password.Any(x => char.IsLower(x))
                && password.Any(x => char.IsUpper(x))
                && password.Any(x => char.IsDigit(x));
        }
    }
}
