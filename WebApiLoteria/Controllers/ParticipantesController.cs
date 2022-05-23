﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpGet]
        [ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            logger.LogInformation("Se obtiene el listado de Participantes");
            return await dbContext.Participantes.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerParticipante")]
        public async Task<ActionResult<GetParticipantesDTO>> GetById(int id)
        {
            logger.LogInformation("Se obtiene Participante por id");

            var participante = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            if (participante == null)
            {
                return NotFound();
            }
            // participante.RifasParticipantes = participante.RifasParticipantes.OrderBy(x => x.Orden).ToList();
            return mapper.Map<GetParticipantesDTO>(participante);
        }

        //[HttpGet("{id:int}", Name = "obtenerParticipantedos")]
        //public async Task<ActionResult<ParticipantesDTOConRifas>> GetById2(int id)
        //{
        //    logger.LogInformation("Se obtiene Participante por id");
        //    var participante = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
        //    participante.RifaParticipante = participante.RifaParticipante.OrderBy(x => x.Orden).ToList();
        //    return mapper.Map<ParticipantesDTOConRifas>(participante);
        //}

        [HttpPost]
        //[ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<ActionResult> Post(ParticipanteDTO participanteDTO)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == participanteDTO.IdRifa);

            if (existe)
            {
                return BadRequest($"Ya existe esa rifa con el id {participanteDTO.IdRifa}");
            }

            var participante = mapper.Map<Participante>(participanteDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            var DTOparticipante = mapper.Map<GetParticipantesDTO>(participante);
            return CreatedAtRoute("obtenerParticipante", new { id = participante.Id }, DTOparticipante);


        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
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
            participanteDB.Id = id;
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


