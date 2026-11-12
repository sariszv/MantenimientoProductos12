using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ProductosAplicacion
    {
        private Conexion? iConexion = null;

        public ProductosAplicacion(Conexion iConexion)
        {
            this.iConexion = iConexion;
        }

        public Dictionary<string, object> CargarTodo()
        {
            var respuesta = new Dictionary<string, object>();
            respuesta["Productos"] = this.iConexion!.Productos!
                .Include(x => x.Categoria)
                .ToList();
            respuesta["Categorias"] = this.iConexion!.Categorias!.ToList();
            respuesta["Proveedores"] = this.iConexion!.Proveedores!.ToList();
            return respuesta;
        }

        public Productos? Guardar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información del producto.");

            entidad.Categoria = null;

            if (entidad.Id == 0)
            {
                this.iConexion!.Productos!.Add(entidad);
            }
            else
            {
                this.iConexion!.Productos!.Update(entidad);
            }
            this.iConexion.SaveChanges();
            return entidad;
        }

        public Productos? Borrar(Productos? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("Producto no válido para eliminar.");

            this.iConexion!.Productos!.Remove(entidad);
            this.iConexion.SaveChanges();
            return entidad;
        }
    }
}
