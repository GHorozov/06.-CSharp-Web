namespace BankSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueStr = value as string;

            var match = Regex.Match(valueStr, @"^[A-Za-z0-9]+[\.\-\_]*[A-Za-z0-9]+@[a-z]+\.[a-z]+$");
            if (!match.Success)
            {
                return false;
            }

            return true;
        }
    }
}
