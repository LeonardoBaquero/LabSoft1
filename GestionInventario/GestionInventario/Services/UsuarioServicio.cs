using System;
using GestionInventario.Models;
using GestionInventario.Repositories;

namespace GestionInventario.Services
{
    public class UsuarioServicio
    {
        public readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioServicio()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }

        public bool ValidarUsuario(Usuario usuario)
        {
            // Obtener el usuario por correo
            var usuarioAlmacenado = _usuarioRepositorio.ObtenerUsuarioPorCorreo(usuario.Correo);

            // Verificar si el usuario existe y si la contraseña coincide
            if (usuarioAlmacenado != null)
            {
                // Aquí deberías usar un método seguro de comparación de contraseñas
                // Por ejemplo, bcrypt o algún otro método de hashing
                // Este es un ejemplo simplificado y no seguro para producción
                return usuarioAlmacenado.Contrasena == usuario.Contrasena;
            }

            return false;
        }

        internal object ObtenerUsuarioPorCorreo(string correo)
        {
            throw new NotImplementedException();
        }
    }
}