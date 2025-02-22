# Imagen base para construir la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos de la solución y los proyectos
COPY *.sln .
COPY src/Coto.VentasAutomoviles.Api/*.csproj src/Coto.VentasAutomoviles.Api/
COPY src/Coto.VentasAutomoviles.Application/*.csproj src/Coto.VentasAutomoviles.Application/
COPY src/Coto.VentasAutomoviles.Domain/*.csproj src/Coto.VentasAutomoviles.Domain/
COPY src/Coto.VentasAutomoviles.Infrastructure/*.csproj src/Coto.VentasAutomoviles.Infrastructure/
COPY src/Coto.VentasAutomoviles.Test/*.csproj src/Coto.VentasAutomoviles.Test/

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish -c Release -o /out

# Imagen más ligera para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar solo los archivos publicados
COPY --from=build /out .

# Exponer puertos
EXPOSE 5000
EXPOSE 5001

# Variable de entorno para la conexión a la DB
ENV ConnectionStrings__DefaultConnection="Server=db;Database=VentasAutomoviles;User Id=sa;Password=Password01!;TrustServerCertificate=true;"

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "Coto.VentasAutomoviles.Api.dll"]


