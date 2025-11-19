using Bogus;
using Bogus.DataSets;
using Domain.Models;

namespace  Tests.Fakers;

public class UserFaker
{
   public static Faker<User> Make()
    {
        return new Faker<User>()
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.CreatedAt, new Date().Past().ToUniversalTime())
            .RuleFor(p => p.Email, new Internet().Email())
            .RuleFor(p => p.PasswordHash, "$2a$11$CPNU0bC9JqSwURZvIz765e1NffJ1wFoZG28YA249ZRdZwsN77GoPK")
            .RuleFor(p => p.Name, new Person().FirstName)
            .RuleFor(p => p.Username, new Internet().UserName());
    }
}
