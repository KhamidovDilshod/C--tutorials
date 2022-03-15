namespace C__tutorials.Utils
{
#pragma warning disable
    public class Util
    {
        public string Encode(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Decode(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}