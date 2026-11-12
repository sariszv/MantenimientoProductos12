using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Proveedores
    {
        [Key]
        public int Id { get; set; }
        public string? Nit { get; set; }
        public string? RazonSocial { get; set; }
        public string? Telefono { get; set; }
    }
}
