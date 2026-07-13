using System.ComponentModel.DataAnnotations;
using DietTracker.Core.Common.Abstractions;

namespace DietTracker.Core.Auth.Settings;

public class JwtSettings : IDietTrackerOptions
{
    public static string SectionName => "Jwt";

    [Required] 
    public required string Key { get; init; }
    
    [Required] 
    public required string Issuer { get; init; }
    
    [Required] 
    public required string Audience { get; init; }
    
    [Required] 
    public required int ExpiresInMinutes { get; init; }
    
    [Required] 
    public required int RefreshExpiresInDays { get; init; }
}