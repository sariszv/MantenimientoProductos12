using lib_aplicaciones.Implementaciones;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosAplicacion _aplicacion;

        public ProductosController(IConfiguration config)
        {
            var conexion = new Conexion
            {
                StringConexion = config.GetConnectionString("DefaultConnection")
            };
            _aplicacion = new ProductosAplicacion(conexion);
        }

        [HttpPost]
        public string CargarTodo()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = _aplicacion.CargarTodo();
                respuesta["Productos"] = datos["Productos"];
                respuesta["Categorias"] = datos["Categorias"];
                respuesta["Proveedores"] = datos["Proveedores"];
                respuesta["Respuesta"] = "OK";
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
            }
            return JsonSerializer.Serialize(respuesta);
        }

        [HttpPost]
        public string Guardar([FromBody] Productos entidad)
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                respuesta["Entidad"] = _aplicacion.Guardar(entidad)!;
                respuesta["Respuesta"] = "OK";
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
            }
            return JsonSerializer.Serialize(respuesta);
        }

        [HttpPost]
        public string Borrar([FromBody] Productos entidad)
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                respuesta["Entidad"] = _aplicacion.Borrar(entidad)!;
                respuesta["Respuesta"] = "OK";
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
            }
            return JsonSerializer.Serialize(respuesta);
        }
    }
}
