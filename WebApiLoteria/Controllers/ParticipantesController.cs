using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoteria.DTOs;
using WebApiLoteria.Entidades;
using WebApiLoteria.Fitros;

namespace WebApiLoteria.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger, IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            logger.LogInformation("Se obtiene el listado de Participantes");
            return await dbContext.Participantes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParticipanteDTO>> GetById(int id)
        {
            logger.LogInformation("Se obtiene Participante por id");
            var participante = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ParticipanteDTO>(participante);
        }

        [HttpPost]
        [ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<ActionResult> Post(CrearParticipanteDTO crearParticipanteDTO)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(x => x.Id == crearParticipanteDTO.RifaId);

            if (!existeRifa)
            {
                return BadRequest($"No existe rifa con el id: {crearParticipanteDTO.RifaId} ");
            }

            var participante = mapper.Map<Participante>(crearParticipanteDTO);
            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CrearParticipanteDTO crearParticipanteDTO, int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El participante no existe");
            }

            var participante = mapper.Map<Participante>(crearParticipanteDTO);
            dbContext.Update(participante);
            await dbContext.SaveChangesAsync();
            return NoContent();
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
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ParticipantePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var participanteDB = await dbContext.Participantes.FirstOrDefaultAsync(X => X.Id == id);

            if (participanteDB == null)
            {
                return NotFound();
            }
            var participanteDTO = mapper.Map<ParticipantePatchDTO>(participanteDB);
            patchDocument.ApplyTo(participanteDTO);
            var esValid = TryValidateModel(participanteDTO);
            if (!esValid)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(participanteDTO, participanteDB);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
