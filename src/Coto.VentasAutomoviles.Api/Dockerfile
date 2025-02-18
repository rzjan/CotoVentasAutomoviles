# Imagen base para construir la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . ./
RUN dotnet publish -c Release -o /out

# Imagen más ligera para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar solo los archivos publicados
COPY --from=build /out .

# Exponer puertos
EXPOSE 5001
EXPOSE 443

# Variable de entorno para la conexión a la DB
ENV ConnectionStrings__DefaultConnection="Server=db;Database=VentasAutomoviles;User Id=sa;Password=Password01!;TrustServerCertificate=true;"

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "TestVentasAutomoviles.dll"]