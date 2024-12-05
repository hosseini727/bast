namespace Raika.Common.SharedApplicationServices.Security
{
    public interface ISecurityServices
    {
        string CreateRandomSalt();
        string CreateRandomString(int size);
        string CreatePasswordHash(string password, string salt);
        string EncryptText(string plainText);
        string DecryptText(string cipherText);
    }
}
