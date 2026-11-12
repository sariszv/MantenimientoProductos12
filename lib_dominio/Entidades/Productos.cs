using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [ForeignKey("CategoriaId")]
        public Categorias? Categoria { get; set; }
    }
}
