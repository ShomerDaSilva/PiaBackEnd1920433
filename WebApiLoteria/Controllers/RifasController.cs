using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoteria.Entidades;

namespace WebApiLoteria.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    public class RifasController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        public RifasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rifa>>> Get()
        {
            return await dbContext.Rifas.Include(x => x.participantes).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Rifa rifa)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(x => x.NombreRifa == rifa.NombreRifa);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Rifa rifa, int id)
        {
            if (rifa.Id != id)
            {
                return BadRequest("El id de la rifa no existe");
            }
            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
