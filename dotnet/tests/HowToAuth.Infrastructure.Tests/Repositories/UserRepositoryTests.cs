using HowToAuth.Core.Entities;
using HowToAuth.Infrastructure.Data.Repositories;
using HowToAuth.Infrastructure.Tests.TestHelpers;

namespace HowToAuth.Infrastructure.Tests.Repositories;

public class UserRepositoryTests : IClassFixture<InfrastructureTestsFixture>
{
    private readonly UserRepository _repository;

    public UserRepositoryTests(InfrastructureTestsFixture fixture)
    {
        _repository = new(fixture.UserManager);
        fixture.ResetDatabase();
    }

    [Fact]
    public async Task GetByEmailAsync_WhenEmailDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var email = "nonexistent@email.com";

        // Act
        var user = await _repository.GetByEmailAsync(email);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenEmailExists_ShouldReturnUser()
    {
        // Arrange
        var email = "existent@email.com";

        await _repository.CreateAsync(new ApplicationUser
        {
            Email = email,
            UserName = email,
        }, "Rand0m$Pass123");

        // Act
        var user = await _repository.GetByEmailAsync(email);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public async Task CheckPasswordAsync_WhenPasswordMatches_ShouldReturnTrue()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
        };

        await _repository.CreateAsync(user, password);

        // Act
        var matchPassword = await _repository.CheckPasswordAsync(user, password);

        // Assert
        Assert.True(matchPassword);
    }

    [Fact]
    public async Task CheckPasswordAsync_WhenPasswordDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
        };

        await _repository.CreateAsync(user, password);

        // Act
        var matchPassword = await _repository.CheckPasswordAsync(user, "dontmatch");

        // Assert
        Assert.False(matchPassword);
    }

    [Fact]
    public async Task CreateAsync_WhenCalled_ShouldReturnSuccessfulResult()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        // Act
        var identityResult = await _repository.CreateAsync(new ApplicationUser
        {
            Email = email,
            UserName = email,
        }, password);

        // Assert
        Assert.NotNull(identityResult);
        Assert.True(identityResult.Succeeded);
    }
}
