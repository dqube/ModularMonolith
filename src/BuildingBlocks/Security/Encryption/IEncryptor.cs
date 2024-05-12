namespace CompanyName.MyProjectName.BuildingBlocks.Security.Encryption;

public interface IEncryptor
{
    string Encrypt(string data, string key = null);

    string Decrypt(string data, string key = null);
}