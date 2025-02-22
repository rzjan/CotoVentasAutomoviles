using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coto.VentasAutomoviles.Api.Service;


public class JwtService
{
    private readonly string _secretKey;
    public JwtService()
    {
        //_secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException("JWT_SECRET_KEY no está configurado.");
        _secretKey = "EstaEsUnaClaveMuySeguraYSecreta123456";
    }   

    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", "1"), new Claim(ClaimTypes.Role, "Admin") }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "CotoVentasAutomoviles.Apps", // Agregar la audiencia aquí
            Issuer = "CotoVentasAutomoviles.Api" // Agregar el emisor aquí
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
