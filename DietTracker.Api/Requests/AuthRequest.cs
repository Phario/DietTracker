namespace DietTracker.Requests;

public record LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public record RegisterRequest : LoginRequest
{
    public required string Name { get; init; }   
}

public record RefreshTokenRequest
{
    public required string RefreshToken { get; init; }
}