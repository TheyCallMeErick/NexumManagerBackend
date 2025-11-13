using Bogus;
using Bogus.DataSets;
using Domain.Models;

namespace Tests.Fakers; 

public class ProjectFaker
{
public static Faker<Project> Make()
    {
        return new Faker<Project>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => new Lorem().Sentence(1,2))
            .RuleFor(p => p.Description, f => new Lorem().Sentence(1,4));
    }
}
