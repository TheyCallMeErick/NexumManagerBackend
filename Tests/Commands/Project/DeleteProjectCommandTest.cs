using Application.Commands.Project;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Tests.Fakers;
using Tests.Utils;
using Task = System.Threading.Tasks.Task;

namespace Tests.Commands.Project;

public class DeleteProjectCommandTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IProjectRepository _projectRepository;
    private readonly DeleteProjectCommand _DeleteProjectCommand;

    public DeleteProjectCommandTest()
    {
        _dbContext = CreateInMemoryDatabase.Handle();
        _projectRepository = new ProjectRepository(_dbContext);
        _DeleteProjectCommand = new DeleteProjectCommand(_projectRepository);
    }

    [Fact]
    public async Task GivenValidData_WhenExecuteIsCalled_ThenShouldDelete()
    {
        //Arrange
        var createData = ProjectFaker.Make().Generate();
        var member = new UserOnProject
        {
            Role = EProjectRole.Admin,
            User= UserFaker.Make().Generate()
        };
        createData.Members.Add(member);
        var data = await _dbContext.Projects.AddAsync(createData);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _DeleteProjectCommand.Execute(data.Entity.Id, member.User.Id);

        //Assert 
        Assert.True(result);
        Assert.Empty(_dbContext.Projects.ToList());
        Assert.Equal(0, _dbContext.Projects.Count());
    }

    [Fact]
    public async Task GivenInvalidData_WhenExecuteIsCalled_ThenShouldntDelete()
    {
        //Arrange
        var createData = ProjectFaker.Make().Generate();
        await _dbContext.Projects.AddAsync(createData);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _DeleteProjectCommand.Execute(Guid.NewGuid(), Guid.NewGuid());

        //Assert 
        Assert.False(result);
        Assert.NotEmpty(_dbContext.Projects.ToList());
        Assert.Equal(1, _dbContext.Projects.Count());
    }
}
