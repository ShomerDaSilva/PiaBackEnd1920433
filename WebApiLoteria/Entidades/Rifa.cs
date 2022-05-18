namespace WebApiLoteria.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        public string NombreRifa { get; set; }

        public List<Participante> participantes { get; set; }
    }
}
