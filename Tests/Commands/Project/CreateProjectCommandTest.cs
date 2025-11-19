using Application.Commands.Project;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Tests.Fakers;
using Tests.Utils;

namespace Tests.Commands.Project; 

public class CreateProjectCommandTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;


    private readonly CreateProjectCommand _createProjectCommand;

    public CreateProjectCommandTest()
    {
        _dbContext = CreateInMemoryDatabase.Handle();
        _projectRepository = new ProjectRepository(_dbContext);
        _userRepository = new UserRepository(_dbContext);
        _createProjectCommand = new CreateProjectCommand(_projectRepository, _userRepository);
    }

    [Fact]
    public async Task GivenValidData_WhenExecuteIsCalled_ThenShouldCreate()
    {
        //Arrange
        var user = UserFaker.Make().Generate();
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        var data = new CreateProjectInputDto(
            ProjectDescription: "Lipsum",
            ProjectName: "Lipsum",
            CurrentUserId: user.Id
        );

        //Act
        var result = await _createProjectCommand.Execute(data);

        //Assert 
        Assert.True(result);
        Assert.NotEmpty(_dbContext.Projects.ToList());
        Assert.Equal(1, _dbContext.Projects.Count());
        Assert.Equal(data.ProjectName, _dbContext.Projects.First().Title);
        Assert.Equal(data.ProjectDescription, _dbContext.Projects.First().Description);
        Assert.NotEmpty(_dbContext.Projects.First().Members.ToList());
        Assert.Equal(EProjectRole.Admin, _dbContext.Projects.First().Members.First().Role);
        Assert.Equal(data.CurrentUserId, _dbContext.Projects.First().Members.First().UserId);
    }

    [Fact]
    public async Task GivenInvalidData_WhenExecuteIsCalled_ThenShouldntCreate()
    {
        //Arrange
        var data = new CreateProjectInputDto(
            ProjectDescription: "",
            ProjectName: "",
            CurrentUserId: Guid.Empty
        );

        //Act
        var result = await _createProjectCommand.Execute(data);

        //Assert 
        Assert.False(result);
        Assert.Empty(_dbContext.Projects.ToList());
        Assert.Empty(_dbContext.UsersOnProjects.ToList());
        Assert.Equal(0, _dbContext.Projects.Count());
    }
}
