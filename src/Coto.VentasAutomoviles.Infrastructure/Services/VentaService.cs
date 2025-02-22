using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Coto.VentasAutomoviles.Domain.Utilities;

namespace Coto.VentasAutomoviles.Infrastructure.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;

    public VentaService(IVentaRepository ventaRepository)
    {
        _ventaRepository = ventaRepository;
    }

    public async Task<Result<Venta>> GenerarVenta(Venta venta)
    {
        try
        {
            return await _ventaRepository.AddAsync(venta);
        }
        catch (Exception ex)
        {
            return Result<Venta>.Failure(ex.Message);
        }
        
    }

    public async Task<Result<List<Venta>>> ObtenerVentasPorCentroDistribucion(int centroDistribucion)
    {
        try
        {
            var ventas = await _ventaRepository.ObtenerVentasPorCentroDistribucion(centroDistribucion);
            return Result<List<Venta>>.Success(ventas);
        }
        catch (Exception ex)
        {
            return Result<List<Venta>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Dictionary<string, object>>> ObtenerVolumenTotalVentas()
    {
        try
        {
            var volumen = await _ventaRepository.ObtenerVolumenTotalVentas();
            return Result<Dictionary<string, object>>.Success(volumen);
        }
        catch (Exception ex)
        {
            return Result<Dictionary<string, object>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Dictionary<string, object>>> ObtenerVolumenVentasPorCentro(int centroDistribucion)
    {
        try
        {
            //Validar si centrodistribucion pertence a la enumeracion
            if (!Enum.IsDefined(typeof(CentroDistribucionEnum), centroDistribucion))
            {                
                string mensajeError = $"El centro de distribución: {centroDistribucion} no es válido";
                return Result<Dictionary<string, object>>.Failure(mensajeError);
               
            }   
            var volumenPorCentro = await _ventaRepository.ObtenerVolumenVentasPorCentro(centroDistribucion);
            return Result<Dictionary<string, object>>.Success(volumenPorCentro);
        }
        catch (Exception ex)
        {
            return Result<Dictionary<string, object>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Dictionary<int, PorcentajeVentasCentroResponse>>> ObtenerPorcentajeVentasPorModeloYCentro()
    {
        try
        {
            // Paso 1: Obtener el total de ventas en la empresa
            var totalVentasEmpresa = await _ventaRepository.ObtenerVolumenTotalVentas();
            int totalCantidadVendida = totalVentasEmpresa.ContainsKey("CantidadTotalVendida")
                ? Convert.ToInt32(totalVentasEmpresa["CantidadTotalVendida"])
                : 0;

            // Verificar que totalCantidadVendida no sea 0
            if (totalCantidadVendida == 0)
            {
                return Result<Dictionary<int, PorcentajeVentasCentroResponse>>.Success(new Dictionary<int, PorcentajeVentasCentroResponse>());
            }

            // Paso 2: Obtener las ventas agrupadas por centro y modelo
            var ventasPorCentroYModelo = _ventaRepository.GetAll()
                .GroupBy(v => new { v.CentroDistribucionId, v.ModeloVehiculoId })
                .Select(g => new
                {
                    CentroDistribucionId = g.Key.CentroDistribucionId,
                    Tipo = (TipoAutomovilEnum)g.Key.ModeloVehiculoId, // Casteo directo
                    TotalCantidad = g.Sum(v => v.Cantidad) // Sumar la columna Cantidad
                })
                .ToList();

            // Paso 3: Calcular el porcentaje de ventas por modelo y centro
            var resultado = ventasPorCentroYModelo
                .GroupBy(v => v.CentroDistribucionId)
                .ToDictionary(
                    g => g.Key,
                    g => new PorcentajeVentasCentroResponse
                    {
                        NombreCentro = Enum.IsDefined(typeof(CentroDistribucionEnum), g.Key)
                            ? ((CentroDistribucionEnum)g.Key).GetDescripcion()
                            : "Centro Desconocido", // Manejar valores no válidos
                        PorcentajeTotalCentro = Math.Round((double)g.Sum(v => v.TotalCantidad) / totalCantidadVendida * 100, 2), // Calcular el porcentaje total por centro
                        CantidadTotalCentro = g.Sum(v => v.TotalCantidad), // Calcular la cantidad total por centro
                        Modelos = g.Select(v => new PorcentajeVentasCentroResponse.ModeloPorcentaje
                        {
                            TipoAutomovil = v.Tipo,
                            Porcentaje = Math.Round((double)v.TotalCantidad / totalCantidadVendida * 100, 2), // Redondeamos a 2 decimales
                            Cantidad = v.TotalCantidad // Incluir la cantidad
                        }).ToList()
                    }
                );

            return Result<Dictionary<int, PorcentajeVentasCentroResponse>>.Success(resultado);
        }
        catch (Exception ex)
        {
            return Result<Dictionary<int, PorcentajeVentasCentroResponse>>.Failure(ex.Message);
        }
    }
}
