﻿using System.ComponentModel.DataAnnotations;
using WebApiLoteria.Validaciones;

namespace WebApiLoteria.DTOs
{
    public class GetParticipantesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string ApellidoP { get; set; }

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string ApellidoM { get; set; }

        public int IdRifa { get; set; }
    }
}
