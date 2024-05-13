using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Kernel;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;

namespace CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;

internal class User : Aggregate<UserId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private User(UserId customerId, string name)
        : base(customerId)
    {
        Name = name;

        // Id= id;
    }

    public string Name { get; private set; }

    public static User Create(UserId userId, string name)
    {
        var customer = new User(userId, name);
        return customer;
    }
}
