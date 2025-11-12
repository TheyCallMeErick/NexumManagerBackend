using Bogus;
using Bogus.DataSets;
using Domain.Models;

namespace  Tests.Fakers;

public class UserFaker
{
   public static Faker<User> Make()
    {
        return new Faker<User>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.CreatedAt, f => new Date().Past().ToUniversalTime())
            .RuleFor(p => p.Email, f => new Internet().Email())
            .RuleFor(p => p.Password, f => "$2a$11$CPNU0bC9JqSwURZvIz765e1NffJ1wFoZG28YA249ZRdZwsN77GoPK")
            .RuleFor(p => p.Name, f => new Person().FirstName)
            .RuleFor(p => p.Username, f => new Internet().UserName());
    }
}
