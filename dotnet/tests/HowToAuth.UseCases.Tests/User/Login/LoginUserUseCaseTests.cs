using HowToAuth.Core.Entities;
using HowToAuth.Core.Interfaces;
using HowToAuth.UseCases.Common;
using HowToAuth.UseCases.User.Login;

namespace HowToAuth.UseCases.Tests.User.Login;

public class LoginUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly LoginUserUseCase _loginUserUseCase;

    public LoginUserUseCaseTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _loginUserUseCase = new LoginUserUseCase(_userRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var request = new LoginUserRequest
        {
            Email = "nonexistent@example.com",
            Password = "password123"
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Contains("Invalid email or password.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenPasswordIsInvalid_ShouldReturnError()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        var request = new LoginUserRequest
        {
            Email = user.Email,
            Password = "wrongpassword"
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        _userRepositoryMock
            .Setup(repo => repo.CheckPasswordAsync(user, request.Password))
            .ReturnsAsync(false);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Contains("Invalid email or password.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCredentialsAreValid_ShouldReturnToken()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        var request = new LoginUserRequest
        {
            Email = user.Email,
            Password = "correctpassword"
        };

        var expectedToken = "generated-jwt-token";

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        _userRepositoryMock
            .Setup(repo => repo.CheckPasswordAsync(user, request.Password))
            .ReturnsAsync(true);

        _tokenServiceMock
            .Setup(service => service.GenerateToken(user))
            .Returns(expectedToken);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedToken, result.Value.Token);
    }
}
