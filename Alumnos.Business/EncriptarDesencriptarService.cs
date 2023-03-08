using Utilerias.Business.Interfaces;

namespace Alumnos.Business
{
    public class EncriptarDesencriptarService : IEncriptarDesencriptarService
    {
        public string Base64Encode(string textoEncode)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(textoEncode);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string textoDecode)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(textoDecode);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}