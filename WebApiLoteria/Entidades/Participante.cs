namespace WebApiLoteria.Entidades
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nombre { get; set;}

        public string ApellidoP { get; set; }

        public string ApellidoM { get; set; }

        //public string Email { get; set;}

        //public int Phone { get; set; }

        public int RifaId { get; set;}

        public Rifa Rifa { get; set; }


    }
}
