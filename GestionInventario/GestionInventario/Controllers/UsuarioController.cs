using Microsoft.AspNetCore.Mvc;
using GestionInventario.Models;
using GestionInventario.Repositories;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }

        [HttpGet("email/{email}")]
        public ActionResult<Usuario> GetByEmail(string email)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuarioPorCorreo(email);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAllUsers()
        {
            return Ok(_usuarioRepositorio.ObtenerListadoUsuarios());
        }

        [HttpPost]
        public ActionResult<Usuario> CrearUsuario(Usuario usuario)
        {
            _usuarioRepositorio.CrearUsuario(usuario);
            return CreatedAtAction(nameof(GetByEmail), new { email = usuario.Correo }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult ModificarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            _usuarioRepositorio.ModificarUsuario(usuario);
            return NoContent();
        }

        [HttpPut("{id}/activar")]
        public IActionResult ActivarUsuario(int id)
        {
            _usuarioRepositorio.ActivarUsuario(id);
            return NoContent();
        }

        [HttpPut("{id}/inactivar")]
        public IActionResult InactivarUsuario(int id)
        {
            _usuarioRepositorio.InactivarUsuario(id);
            return NoContent();
        }
    }
}