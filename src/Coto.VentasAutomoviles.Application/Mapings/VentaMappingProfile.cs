using AutoMapper;
using Coto.VentasAutomoviles.Domain.Entities;

namespace Coto.VentasAutomoviles.Application.Mapings;

public class VentaMappingProfile : Profile
{
    public VentaMappingProfile()
    {
        CreateMap<Venta, VentaDto>().ReverseMap();
    }
}

