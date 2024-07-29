namespace ContosoPizza.Models;

public class Login
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required Boolean RememberMe { get; set; }
}
