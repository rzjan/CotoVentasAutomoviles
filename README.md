# README - Coto Ventas Automóviles

## Programas Requeridos
Antes de comenzar, asegúrate de tener instalados los siguientes programas:

- **Docker** (Instalador disponible en [Docker](https://www.docker.com/get-started))
- **Visual Studio** (Recomendado: VS 2019 o superior)
- **SQL Server**
- **SQL Server Management Studio (SSMS)**

## Pasos para Configurar y Ejecutar el Proyecto

### 1. Clonar el Repositorio desde GitHub
```sh
git clone https://github.com/rzjan/CotoVentasAutomoviles.git
```

### 2. Generar los Contenedores con Docker
#### 2.1. Ejecutar en Docker
1. Abrir una terminal **PowerShell** como administrador (Click derecho > Ejecutar como administrador).
2. Ubicarse en el directorio donde se encuentra el archivo `docker-compose.yml` del proyecto clonado:
    ```sh
    cd [directorio]
    ```
3. Ejecutar el siguiente comando para compilar y levantar los contenedores:
    ```sh
    docker-compose up --build -d
    ```
4. Como resultado, los contenedores deberían estar en estado **Up**. Puedes verificar con:
    ```sh
    docker ps
    ```

#### 2.2. Alternativa sin Docker: Ejecutar las Migraciones en Base de Datos Local
1. Abrir el archivo `appsettings.json` y asegurarse de que la cadena de conexión **DesaLocalConnection** esté configurada correctamente.
2. Desde la **Consola del Administrador de Paquetes** en Visual Studio:
    - Seleccionar el proyecto `Coto.VentasAutomoviles.Infrastructure` como proyecto predeterminado.
    - Asegurar que en la lista de ejecución se seleccione `Coto.VentasAutomoviles.Api`.
    - Ejecutar los siguientes comandos:
    ```sh
    $env:JWT_SECRET_KEY="EstaEsUnaClaveMuySeguraYSecreta123456"
    update-database
    ```

### 3. Correr las Migraciones de la Base de Datos
1. Abrir la solución en **Visual Studio**.
2. Verificar la configuración de la cadena de conexión en `appsettings.json`:
    ```json
    "DesaConnection": "Server=localhost,1433;Database=VentasAutomoviles;User Id=sa;Password=Password01!;TrustServerCertificate=True;"
    ```
    *Nota:* Asegúrate de que el servidor sea `Server=localhost,1433`. Cambiar si es necesario.
3. Desde la **Consola del Administrador de Paquetes NuGet**, ejecutar los siguientes comandos:
    ```sh
    $env:JWT_SECRET_KEY="EstaEsUnaClaveMuySeguraYSecreta123456"
    update-database
    ```
4. Si deseas volver a ejecutar en Docker, cambia nuevamente la cadena de conexión a:
    ```json
    "DesaConnection": "Server=db"
    ```
5. Luego, recompilar los contenedores de Docker:
    ```sh
    docker-compose up --build -d
    ```

### 4. Ejecutar la Aplicación
1. Abrir el navegador y acceder a Swagger para probar la API:
    ```sh
    http://localhost:5003/swagger/index.html
    ```

### 5. Autenticación y Uso de los Endpoints
1. Ejecutar el endpoint de autenticación para obtener un token JWT:
    ```sh
    http://localhost:5003/api/auth/token
    ```
2. Copiar el token generado.
3. En **Swagger**, hacer clic en el botón **Authorize** y pegar el token en el siguiente formato:
    ```sh
    Bearer [TOKEN]
    ```
4. Seguir la documentación de **Swagger** para utilizar los endpoints de la API.

---

**Gracias por revisar este proyecto. Cualquier comentario o sugerencia es bienvenido.** 🚀

