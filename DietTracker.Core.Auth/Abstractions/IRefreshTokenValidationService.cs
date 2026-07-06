namespace DietTracker.Core.Auth.Abstractions;

public interface IRefreshTokenValidationService
{
    Task<bool> ValidateTokenAsync(Guid userId, string refreshToken);
}