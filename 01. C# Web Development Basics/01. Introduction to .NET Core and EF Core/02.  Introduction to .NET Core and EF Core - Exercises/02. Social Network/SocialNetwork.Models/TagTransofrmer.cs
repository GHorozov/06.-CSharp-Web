namespace SocialNetwork.Models
{
    public class TagTransofrmer
    {
        public static string Transform(string tag)
        {
            if (!tag.StartsWith("#"))
            {
                tag = "#" + tag;
            }
            else if(tag.Contains(" "))
            {
                tag = tag.Replace(" ", "");
            }
            else if(tag.Length > 20)
            {
                tag = tag.Substring(0, 19);
            }

            return tag;
        }
    }
}
