using Microsoft.AspNetCore.Mvc;
using GestionInventario.Models;
using GestionInventario.Services;
using System; 

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticarController : ControllerBase
    {
        private readonly UsuarioServicio _usuarioServicio;

        public AutenticarController()
        {
            _usuarioServicio = new UsuarioServicio();
        }

        [HttpPost("validar")]
        public IActionResult ValidarUsuario([FromBody] CredencialesUsuario credenciales)
        {
            var usuario = new Usuario { Correo = credenciales.Correo, Contrasena = credenciales.Password };
            bool esValido = _usuarioServicio.ValidarUsuario(usuario);

            if (esValido)
            {
                var usuarioAutenticado = _usuarioServicio.ObtenerUsuarioPorCorreo(credenciales.Correo);
                return Ok(new
                {
                    AutenticacionExitosa = true,
                    Jwt = Guid.NewGuid().ToString(),
                    //Mensaje = $"Bienvenido {usuarioAutenticado.Nombre} {usuarioAutenticado.Apellido}"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    AutenticacionExitosa = false,
                    Jwt = (string)null,
                    Mensaje = "Error al autenticar el usuario"
                });
            }
        }
    }

    public class CredencialesUsuario
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}