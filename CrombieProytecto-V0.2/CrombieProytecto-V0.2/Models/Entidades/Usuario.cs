using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class Usuario : BaseEntity

    {
        public required string Nombre { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
        public required string PasswordHash { get; set; }
        public string Salt { get; set; }
        public UserRole Roles { get; set; }
        public virtual ICollection<WishList>? WishLists { get; set; }
    }

    public enum UserRole
    {
        Regular,
        Admin
    }
}
