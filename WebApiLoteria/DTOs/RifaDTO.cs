using System.ComponentModel.DataAnnotations;

namespace WebApiLoteria.DTOs
{
    public class RifaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string NombreRifa { get; set; }

    }
}
