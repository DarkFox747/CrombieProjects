using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._2.Models.dtos
{
    public class PaginationParameters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;
    }
}




