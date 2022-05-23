using System.ComponentModel.DataAnnotations;

namespace WebApiLoteria.Entidades
{
    public class SistemaUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
