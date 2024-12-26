using System.Text;

namespace BaseProject.Core.Token
{
    public class TokenHandler
    {
        private static string SecretKey = "6798d515f9984fe3a1c8412c26271450";
        public static byte[] GenerateSecretByte() =>
        Encoding.ASCII.GetBytes(SecretKey);
    }
}
