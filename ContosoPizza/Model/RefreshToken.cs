using ContosoPizza.Helprs;

namespace ContosoPizza.Models;
public class RefreshToken
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime Created { get; set; }

    public bool IsActive => !IsRevoked && Expires > DateTime.UtcNow;
}