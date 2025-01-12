namespace CrombieProytecto_V0._1.Models.dtos
{
    public class WishListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProductoDto> Productos { get; set; }
    }
}
