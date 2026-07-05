using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.Users;

public class RefreshTokenEntity
{
    public Guid Id { get; }
    public required string Token { get; init; }
    public required DateTime ExpiresAt { get; init; }
    public required Guid UserId { get; set; }
    public UserEntity User { get; set; }
}

internal class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(p => p.User)
            .WithOne(u => u.RefreshToken)
            .HasForeignKey<RefreshTokenEntity>(p => p.UserId);
    }
}