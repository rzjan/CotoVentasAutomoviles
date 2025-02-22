using Coto.VentasAutomoviles.Application.Features.Commands;
using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Interfaces;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.CommandsHandler;

public class GenerarVentaHandler : IRequestHandler<GenerarVentaCommand, bool>
{
    private readonly IVentaRepository _ventaRepository;
    private readonly Func<TipoAutomovilEnum, IPreciosStrategy> _preciosStrategyFactory;

    public GenerarVentaHandler(IVentaRepository ventaRepository, Func<TipoAutomovilEnum, IPreciosStrategy> preciosStrategyFactory)
    
    {
        _ventaRepository = ventaRepository;
        _preciosStrategyFactory = preciosStrategyFactory;
    }

    public async Task<bool> Handle(GenerarVentaCommand request, CancellationToken cancellationToken)
    {
        // Obtener el precio base del automóvil y calcular el precio final aplicando interes si require
        var preciosStrategy = _preciosStrategyFactory(request.TipoAutomovil);
        var (precioBase, precioFinal) = preciosStrategy.CalcularPrecio(request.TipoAutomovil);

        // Crear la entidad de venta
        var venta = new Venta
        {
            CentroDistribucionId = request.CentroDistribucion,            
            FechaDeVenta = DateTime.UtcNow,
            ModeloVehiculoId = (int)request.TipoAutomovil,
            Cantidad= request.Cantidad,
            PrecioBase = precioBase,
            PrecioDeVenta = precioFinal * request.Cantidad,
            ClienteId = request.ClienteId,
        };

        // Guardar en base de datos
        var result = await _ventaRepository.AddAsync(venta);        
        if (!result.IsSuccess)
        {
            // Lanzar una excepción con el mensaje de error
            throw new Exception(result.Error);
        }
        return true;
    }
}