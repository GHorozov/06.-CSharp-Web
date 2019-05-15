namespace MyCoolWebServer.GameStoreApplication.ViewModels.Account
{
    using MyCoolWebServer.GameStoreApplication.Common;
    using MyCoolWebServer.GameStoreApplication.Utilities;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        [Utilities.EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Full name")]
        [MinLength(ValidationConstants.Account.NameMinLength, ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Account.NameMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength, ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        [Password]
        public string Password { get; set; }

        [Display(Name ="Confirm password")]
        [Compare(nameof(Password))] 
        public string ConfirmPassword { get; set; }
    }
}
