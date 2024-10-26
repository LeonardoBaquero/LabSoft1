using GestionInventario.Models;
using GestionInventario.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productos = await _productoRepositorio.GetAllProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener productos: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoRepositorio.GetProductoByIdAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener el producto: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errores = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                    return BadRequest(new { Errores = errores });
                }

                producto.Id = 0;
                producto.Proveedor = null;

                try
                {
                    var createdProducto = await _productoRepositorio.CreateProductoAsync(producto);
                    return CreatedAtAction(
                        nameof(GetProducto),
                        new { id = createdProducto.Id },
                        createdProducto
                    );
                }
                catch (InvalidOperationException ex)
                {
                    // Este catch específico manejará el error de proveedor no existente
                    return BadRequest(new { Mensaje = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Mensaje = "Error al crear el producto",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest(new { mensaje = "El ID del producto no coincide" });
                }

                var existingProduct = await _productoRepositorio.GetProductoByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }

                await _productoRepositorio.UpdateProductoAsync(producto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al actualizar el producto: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _productoRepositorio.GetProductoByIdAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }

                await _productoRepositorio.DeleteProductoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar el producto: {ex.Message}" });
            }
        }
    }
}