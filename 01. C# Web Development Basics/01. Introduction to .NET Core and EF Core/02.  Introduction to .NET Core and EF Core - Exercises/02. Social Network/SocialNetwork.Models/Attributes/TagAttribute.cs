namespace SocialNetwork.Models.Attributes
{
    public class TagAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueStr = value as string;

            if (!valueStr.StartsWith("#") || valueStr.Length > 20)
            {
                return false;
            }

            return true;
        }
    }
}
