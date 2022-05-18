using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoteria.Entidades;

namespace WebApiLoteria.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController: ControllerBase 
    {
        private readonly ApplicationDbContext dbContext;
        public ParticipantesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            return await dbContext.Participantes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Participante>> GetById(int id)
        {
            return await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
        }

       [HttpPost]
        public async Task<ActionResult> Post(Participante participante)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(x => x.Id == participante.RifaId);
            if (!existeRifa)
            {
                return BadRequest($"No existe rifa con el id: {participante.RifaId} ");
            } 
            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(Participante participante, int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El participante no existe");
            }
            if (participante.Id != id)
            {
                return BadRequest("El id no existe");
            }
            dbContext.Update(participante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("No se encontro el participante");
            }
            dbContext.Remove(new Participante { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
