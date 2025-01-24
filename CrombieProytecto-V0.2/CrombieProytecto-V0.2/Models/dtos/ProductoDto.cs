namespace CrombieProytecto_V0._2.Models.dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? URL { get; set; }
        public List<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();
    }
}
