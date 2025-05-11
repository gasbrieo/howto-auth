namespace HowToAuth.Infrastructure.Identity;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() =>
        new() { Code = "", Description = "An unknown error occurred." };

    public override IdentityError ConcurrencyFailure() =>
        new() { Code = "", Description = "The data was updated by another process. Please reload." };

    public override IdentityError PasswordMismatch() =>
        new() { Code = "password", Description = "Incorrect password." };

    public override IdentityError InvalidToken() =>
        new() { Code = "token", Description = "Invalid or expired token." };

    public override IdentityError RecoveryCodeRedemptionFailed() =>
        new() { Code = "token", Description = "Recovery code could not be used." };

    public override IdentityError LoginAlreadyAssociated() =>
        new() { Code = "email", Description = "This login is already associated with an account." };

    public override IdentityError InvalidUserName(string? userName) =>
        new() { Code = "email", Description = "The email must not contain invalid characters." };

    public override IdentityError InvalidEmail(string? email) =>
        new() { Code = "email", Description = "The email address is invalid." };

    public override IdentityError DuplicateUserName(string userName) =>
        new() { Code = "email", Description = "This email is already registered." };

    public override IdentityError DuplicateEmail(string email) =>
        new() { Code = "email", Description = "This email is already registered." };

    public override IdentityError InvalidRoleName(string? role) =>
        new() { Code = "role", Description = "The specified role is invalid." };

    public override IdentityError DuplicateRoleName(string role) =>
        new() { Code = "role", Description = "The role already exists." };

    public override IdentityError UserAlreadyHasPassword() =>
        new() { Code = "password", Description = "User already has a password." };

    public override IdentityError UserLockoutNotEnabled() =>
        new() { Code = "email", Description = "Lockout is not enabled for this account." };

    public override IdentityError UserAlreadyInRole(string role) =>
        new() { Code = "role", Description = $"User already has the role '{role}'." };

    public override IdentityError UserNotInRole(string role) =>
        new() { Code = "role", Description = $"User does not have the role '{role}'." };

    public override IdentityError PasswordTooShort(int length) =>
        new() { Code = "password", Description = $"The password must be at least {length} characters long." };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        new() { Code = "password", Description = $"The password must contain at least {uniqueChars} unique characters." };

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        new() { Code = "password", Description = "The password must contain at least one symbol (!, @, #, etc.)." };

    public override IdentityError PasswordRequiresDigit() =>
        new() { Code = "password", Description = "The password must contain at least one number." };

    public override IdentityError PasswordRequiresLower() =>
        new() { Code = "password", Description = "The password must contain at least one lowercase letter." };

    public override IdentityError PasswordRequiresUpper() =>
        new() { Code = "password", Description = "The password must contain at least one uppercase letter." };
}
