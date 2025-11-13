using Application.Commands.Project;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Tests.Utils;

namespace Tests.Commands.Project; 

public class CreateProjectCommandTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IProjectRepository _projectRepository;
    private readonly CreateProjectCommand _createProjectCommand;

    public CreateProjectCommandTest()
    {
        _dbContext = CreateInMemoryDatabase.Handle();
        _projectRepository = new ProjectRepository(_dbContext);
        _createProjectCommand = new CreateProjectCommand(_projectRepository);
    }

    [Fact]
    public async Task GivenValidData_WhenExecuteIsCalled_ThenShouldCreate()
    {
        //Arrange
        var data = new CreateProjectInputDTO(
            ProjectDescription: "Lipsum",
            ProjectName: "Lipsum"
        );

        //Act
        var result = await _createProjectCommand.Execute(data);

        //Assert 
        Assert.True(result);
        Assert.NotEmpty(_dbContext.Projects.ToList());
        Assert.Equal(data.ProjectName, _dbContext.Projects.First().Title);
        Assert.Equal(data.ProjectDescription, _dbContext.Projects.First().Description);
        Assert.Equal(1, _dbContext.Projects.Count());
    }

    [Fact]
    public async Task GivenInvalidData_WhenExecuteIsCalled_ThenShouldntCreate()
    {
        //Arrange
        var data = new CreateProjectInputDTO(
            ProjectDescription: "",
            ProjectName: ""
        );

        //Act
        var result = await _createProjectCommand.Execute(data);

        //Assert 
        Assert.False(result);
        Assert.Empty(_dbContext.Projects.ToList());
        Assert.Equal(0, _dbContext.Projects.Count());
    }
}
