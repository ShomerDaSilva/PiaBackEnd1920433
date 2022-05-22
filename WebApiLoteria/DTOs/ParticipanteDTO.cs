namespace WebApiLoteria.DTOs
{
    public class ParticipanteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }


        public string ApellidoP { get; set; }


        public string ApellidoM { get; set; }

        public DateTime FechaInscripcion { get; set; }
        public int RifaId { get; set; }
    }
}
