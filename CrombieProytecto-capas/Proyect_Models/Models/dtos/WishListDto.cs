namespace Proyect_Models.Models.dtos
{
    public class WishListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProductoDto> Productos { get; set; }
    }
}
