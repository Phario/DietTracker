namespace DietTracker.Core.Users.Results;

public class TokenDto
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }   
}