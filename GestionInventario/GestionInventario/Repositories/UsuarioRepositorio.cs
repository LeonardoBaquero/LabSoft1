using System;
using System.Collections.Generic;
using System.Linq;
using GestionInventario.Models;

namespace GestionInventario.Repositories
{
    public class UsuarioRepositorio
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public void CrearUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.TipoDocumento = usuario.TipoDocumento;
                usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;
                usuarioExistente.Direccion = usuario.Direccion;
                usuarioExistente.Telefono = usuario.Telefono;
                usuarioExistente.EstadoActivo = usuario.EstadoActivo;
                usuarioExistente.Contrasena = usuario.Contrasena;
            }
        }

        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            return usuarios.FirstOrDefault(u => u.Correo == correo);
        }

        public List<Usuario> ObtenerListadoUsuarios()
        {
            return usuarios.ToList();
        }

        public void ActivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuario.EstadoActivo = true;
            }
        }

        public void InactivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuario.EstadoActivo = false;
            }
        }
    }
}