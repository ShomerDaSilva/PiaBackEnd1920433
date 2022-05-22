using AutoMapper;
using WebApiLoteria.DTOs;
using WebApiLoteria.Entidades;

namespace WebApiLoteria.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearRifaDTO, Rifa>();
            CreateMap<Rifa, RifaDTO>();
            CreateMap<CrearParticipanteDTO, Participante>();
            CreateMap<Participante, ParticipanteDTO>();
            CreateMap<ParticipantePatchDTO, Participante>().ReverseMap();
        }
    }
}
