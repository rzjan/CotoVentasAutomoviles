using Api.Controllers;
using Coto.VentasAutomoviles.Application.DTOs;
using Coto.VentasAutomoviles.Application.Features.Commands;
using Coto.VentasAutomoviles.Application.Features.Queries;
using Coto.VentasAutomoviles.Application.Request;
using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class VentasControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly VentasController _controller;

    public VentasControllerTests()
    {
        var logger = new Mock<ILogger<VentasController>>().Object;
        _mediatorMock = new Mock<IMediator>();
        _controller = new VentasController(_mediatorMock.Object, logger);
    }

    [Fact]
    public async Task GenerarVenta_ReturnsOkResult_WhenVentaIsGenerated()
    {
        // Arrange
        var request = new GenerarVentaRequest
        {
            TipoAutomovil = TipoAutomovilEnum.Sedan,
            CentroDistribucion = 1,
            Cantidad = 10,
            FechaVenta = DateTime.Now,
            ClienteId = 123
        };
        var command = new GenerarVentaCommand(
            request.TipoAutomovil,
            request.CentroDistribucion,
            request.Cantidad,
            request.FechaVenta,
            request.ClienteId
        );
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerarVentaCommand>(), default)).ReturnsAsync(true);

        // Act
        var result = await _controller.GenerarVenta(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Venta registrada exitosamente.", okResult.Value);
    }

    [Fact]
    public async Task GenerarVenta_ReturnsBadRequest_WhenVentaIsNotGenerated()
    {
        // Arrange
        var request = new GenerarVentaRequest
        {
            TipoAutomovil = TipoAutomovilEnum.Sedan,
            CentroDistribucion = 1,
            Cantidad = 10,
            FechaVenta = DateTime.Now,
            ClienteId = 123
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerarVentaCommand>(), default)).ReturnsAsync(false);

        // Act
        var result = await _controller.GenerarVenta(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("No se pudo generar la venta.", badRequestResult.Value);
    }

    [Fact]
    public async Task GenerarVenta_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new GenerarVentaRequest
        {
            TipoAutomovil = TipoAutomovilEnum.Sedan,
            CentroDistribucion = 1,
            Cantidad = 10,
            FechaVenta = DateTime.Now,
            ClienteId = 123
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerarVentaCommand>(), default)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GenerarVenta(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Equal("Error interno del servidor: Test exception", statusCodeResult.Value);
    }


    [Fact]
    public async Task ObtenerVolumenTotalDeVentas_ReturnsOk_WithResult()
    {
        // Arrange
        var expectedResponse = new VolumenTotalVentasResponse
        {
            CantidadTotalVendida = 100,
            MontoTotalVendido = 100000m
        };
        var expectedResult = Result<VolumenTotalVentasResponse>.Success(expectedResponse);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ObtenerVolumenTotalVentasQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.ObtenerVolumenTotalDeVentas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResult = Assert.IsType<Result<VolumenTotalVentasResponse>>(okResult.Value);

        Assert.True(actualResult.IsSuccess);
        Assert.Equal(expectedResponse.CantidadTotalVendida, actualResult.Value.CantidadTotalVendida);
        Assert.Equal(expectedResponse.MontoTotalVendido, actualResult.Value.MontoTotalVendido);
    }

    [Fact]
    public async Task ObtenerVolumenTotalDeVentas_ReturnsOkResult_WithExpectedData()
    {
        // Arrange
        var expectedResponse = new VolumenTotalVentasResponse
        {
            CantidadTotalVendida = 100,
            MontoTotalVendido = 100000m
        };
        var expectedResult = Result<VolumenTotalVentasResponse>.Success(expectedResponse);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ObtenerVolumenTotalVentasQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.ObtenerVolumenTotalDeVentas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResult = Assert.IsType<Result<VolumenTotalVentasResponse>>(okResult.Value);

        Assert.True(actualResult.IsSuccess);
        Assert.Equal(expectedResponse.CantidadTotalVendida, actualResult.Value.CantidadTotalVendida);
        Assert.Equal(expectedResponse.MontoTotalVendido, actualResult.Value.MontoTotalVendido);
    }

    [Fact]
    public async Task ObtenerVolumenDeVentasPorCentro_ReturnsOk_WithResult()
    {
        // Arrange
        var request = new CentroDistribucionDto { IdCentro = 1 };
        var expectedResult = Result<VolumenVentasPorCentroResponse>.Success(new VolumenVentasPorCentroResponse
        {
            CentroDistribucion = 1,
            VolumenVentas = new VolumenTotalVentasResponse
            {
                CantidadTotalVendida = 100,
                MontoTotalVendido = 100000m
            }
        });
        _mediatorMock.Setup(m => m.Send(It.IsAny<ObtenerVolumenVentasPorCentroQuery>(), default))
                     .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.ObtenerVolumenDeVentasPorCentro(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var foo2 = Assert.IsType<Result<VolumenVentasPorCentroResponse>>(okResult.Value);
        Assert.Equal(expectedResult.Value.CentroDistribucion, foo2.Value.CentroDistribucion);
        Assert.Equal(expectedResult.Value.VolumenVentas, foo2.Value.VolumenVentas);
    }

    [Fact]
    public async Task ObtenerPorcentajeDeVentasPorModeloYCentro_ReturnsOk_WithResult()
    {
        // Arrange
        var expectedResult = Result<PorcentajeVentasResponse>.Success(
            new PorcentajeVentasResponse
            {
                Centros = new Dictionary<int, PorcentajeVentasResponse.CentroVentas>
                {
                    { 1, new PorcentajeVentasResponse.CentroVentas
                        {
                            NombreCentro = "Centro Norte",
                            PorcentajeTotalCentro = 50.0,
                            Modelos = new List<PorcentajeVentasResponse.ModeloPorcentaje>
                            {
                                new PorcentajeVentasResponse.ModeloPorcentaje { TipoAutomovil = TipoAutomovilEnum.Sedan, Porcentaje = 50.0 }
                            }
                        }
                    }
                }
            });
        _mediatorMock.Setup(m => m.Send(It.IsAny<ObtenerPorcentajesVentasQuery>(), default)).ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.ObtenerPorcentajeDeVentasPorModeloYCentro();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualResult = Assert.IsType<Result<PorcentajeVentasResponse>>(okResult.Value);

        Assert.True(actualResult.IsSuccess);
        Assert.Equal(expectedResult.Value.Centros[1].NombreCentro, actualResult.Value.Centros[1].NombreCentro);
        Assert.Equal(expectedResult.Value.Centros[1].PorcentajeTotalCentro, actualResult.Value.Centros[1].PorcentajeTotalCentro);
        Assert.Equal(expectedResult.Value.Centros[1].Modelos[0].TipoAutomovil, actualResult.Value.Centros[1].Modelos[0].TipoAutomovil);
        Assert.Equal(expectedResult.Value.Centros[1].Modelos[0].Porcentaje, actualResult.Value.Centros[1].Modelos[0].Porcentaje);
    }
}
