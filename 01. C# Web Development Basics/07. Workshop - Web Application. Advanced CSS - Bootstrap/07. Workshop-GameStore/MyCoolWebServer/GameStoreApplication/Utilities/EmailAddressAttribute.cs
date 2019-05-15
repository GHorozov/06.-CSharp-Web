namespace MyCoolWebServer.GameStoreApplication.Utilities
{
    using System.ComponentModel.DataAnnotations;

    public class EmailAddressAttribute : ValidationAttribute
    {
        public EmailAddressAttribute()
        {
            this.ErrorMessage = "Invalid email address.";
        }

        public override bool IsValid(object value)
        {
            var email = value as string;
            if (email == null)
            {
                return true;
            }

            return email.Contains("@")
                && email.Contains(".");
        }
    }
}
