namespace Utilerias.Business.Interfaces
{
    public interface IEncriptarDesencriptarService
    {
        string Base64Encode(string textoEncode);

        string Base64Decode(string textoDecode);
    }
}