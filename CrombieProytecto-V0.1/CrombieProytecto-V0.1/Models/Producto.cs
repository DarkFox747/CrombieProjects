
namespace CrombieProytecto_V0._1.Models



{
    public class Producto
    {
        
        public int Id { get; set; }

        public string Nombre { get; set; } 
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        public DateTime Activo { get; set; }

        public string Categoria { get; set; }

        public int IdPrecio    { get; set; }



    }
}
