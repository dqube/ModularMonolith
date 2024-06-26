namespace CompanyName.MyProjectName.BuildingBlocks.Security.Hashing;

public interface IShaHasher
{
    string Sha256(string data);

    string Sha512(string data);
}