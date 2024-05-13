using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Exceptions;

namespace CompanyName.MyProjectName.Modules.Users.Domain.Users.Exceptions;

public class UsertNotFoundException(int userId) : CustomException($"User with ID: '{userId}' was not found.")
{
    public int UserId { get; } = userId;
}