using Coto.VentasAutomoviles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coto.VentasAutomoviles.Infrastructure.Data.Config;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Ventas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.CentroDistribucionId)
            .IsRequired();

        builder.Property(v => v.ModeloVehiculoId)
            .IsRequired();

        builder.Property(v => v.Cantidad)
            .IsRequired();

        builder.Property(v => v.FechaDeVenta)
            .IsRequired();

        builder.Property(v => v.PrecioDeVenta)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(v => v.PrecioBase)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(v => v.ClienteId)
            .IsRequired();

        builder.Property(v => v.Vehiculo)
            .HasConversion<string>()
            .IsRequired();
    }
}
