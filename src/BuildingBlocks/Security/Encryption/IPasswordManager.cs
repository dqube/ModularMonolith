namespace CompanyName.MyProjectName.BuildingBlocks.Security.Encryption;

public interface IPasswordManager
{
    string Secure(string password);

    bool IsValid(string password, string securedPassword);
}