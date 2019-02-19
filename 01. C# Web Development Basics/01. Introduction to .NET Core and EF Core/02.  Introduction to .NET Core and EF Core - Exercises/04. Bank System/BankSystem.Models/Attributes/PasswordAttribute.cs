namespace BankSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueStr = value as string;

            var match = Regex.Match(valueStr, @"^[A-Za-z0-9]{6,}$");
            if (!match.Success)
            {
                return false;
            }

            return true;
        }
    }
}
