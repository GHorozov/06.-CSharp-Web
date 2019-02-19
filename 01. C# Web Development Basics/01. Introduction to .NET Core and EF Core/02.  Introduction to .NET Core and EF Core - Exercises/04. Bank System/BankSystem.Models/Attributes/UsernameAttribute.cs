namespace BankSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class UsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueStr = value as string;

            var match = Regex.Match(valueStr, @"^[^0-9][A-Za-z0-9]{3,}$");
            if (!match.Success)
            {
                return false;
            }

            return true;
        }
    }
}
