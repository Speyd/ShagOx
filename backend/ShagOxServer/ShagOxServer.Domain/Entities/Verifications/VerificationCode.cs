using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Verifications;
public class VerificationCode 
    : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string CodeHash { get; set; } = "";

    public DateTime ExpiresAt { get; set; }

    public DateTime? UsedAt { get; set; }

    public int Attempts { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public override string ToString()
    {
        return $"Created: {CreatedAt} | Attempts: {Attempts}";
    }
}