using Coto.VentasAutomoviles.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Coto.VentasAutomoviles.Api.Controllers;

[ApiController]
[Route("api/auth")]
/// <summary>
/// Controlador de autorización para pruebas técnicas.
/// </summary>
/// <remarks>
/// Este controlador usa un token **Predefinido** solo con fines de prueba técnica.  
/// No se implementa autenticación con usuarios.
/// </remarks>
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController()
    {
        _jwtService = new JwtService();
    }

    /// <summary>
    /// Genera un token de autenticación de prueba.
    /// </summary>
    /// <remarks>
    /// Este controlador usa un token **Predefinido** solo con fines de prueba técnica.  
    /// En esta prueba técnica, el token se genera manualmente sin validación de usuarios.
    /// </remarks>
    [HttpGet("token")]
    public IActionResult GetToken()
    {
        var token = _jwtService.GenerateToken();
        return Ok(new { Token = token });
    }
}
