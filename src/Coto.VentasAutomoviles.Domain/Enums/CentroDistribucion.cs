namespace Coto.VentasAutomoviles.Domain.Enums;

public enum CentroDistribucionEnum
{
    CentroNorte,
    CentroSur,
    CentroEste,
    CentroOeste
}

public static class CentroDistribucionEnumExtensions
{
    public static string GetDescripcion(this CentroDistribucionEnum centro)
    {
        return centro switch
        {
            CentroDistribucionEnum.CentroNorte => "Centro Norte",
            CentroDistribucionEnum.CentroSur => "Centro Sur",
            CentroDistribucionEnum.CentroEste => "Centro Este",
            CentroDistribucionEnum.CentroOeste => "Centro Oeste",
            _ => throw new ArgumentOutOfRangeException(nameof(centro), centro, null)
        };
    }
}