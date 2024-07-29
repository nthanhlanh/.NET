// LoginResponse.cs
using ContosoPizza.Models;

namespace ContosoPizza.Dto;
public class LoginResponse
{
    public  required string Token { get; set; }
    public required RefreshToken RefreshToken { get; set; }
}
