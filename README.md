# rest-api-product-asp-dotnet
# MiApiRestTest - Documentación
### Descripción del proyecto
Este proyecto es una API RESTful hecha en ASP.NET Core, diseñada para la gestión de productos de un sistema. La API permite crear, obtener, actualizar y eliminar productos, y está dockerizada junto a una base de datos PostgreSQL.

# Características:
- Gestión de productos con CRUD.
- Validación de modelos.
- Manejo de resultados con tipos discriminados (Result<ProductModel>) para mantner un control sobre las respuestas exitosas y fallidas.
- Manejo de errores con códigos HTTP.
- Documentación automática de la API con Swagger.
- Pruebas unitarias utilizando xUnit y Moq.

##### Manejo de Resultados con ```Result<ProductModel>```
En el proyecto se ha implementado un patrón para manejar los resultados de las operaciones, utilizando un tipo discriminado Result<T>. Esto permite que cada operación retorne un objeto que claramente indica si la operación fue exitosa o fallida, y facilita el manejo de errores en la API.

**Estructura del Result<T>**
**El tipo Result<T> tiene la siguiente estructura:**
```
public class Result<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }    // Si es exitoso, muestra los datos acaá
    public string ErrorMessage { get; set; } // Mensaje de error si es que falla
}
```
# Tecnologías:
- ASP.NET Core 
- EntityFrameworkCore 
- PostgreSQL 
- Docker 
- xUnit 
- Moq 
- Swagger
# Instalación y setup
##### Requisitos:
- .NET SDK (Versión 8.0 o superior)
- Docker
- PostgreSQL (En el contenedor de Docker o localmente)
# Instrucciones para instalar
1. **Clona el repositorio de la aplicación:**
```
git clone https://github.com/usuario/MiApiRestTest.git
cd MiApiRestTest
```
2. **Configura el archivo appsettings.json**
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=miapidb;Username=postgres;Password=password"
  }
}
```
3. **Agrega una migracioón**
```
dotnet ef migrations add InitialCreate
```
4. **Dockeriza la applicación**
##### Primero, asegúrate de tener Docker instalado y corriendo.
- https://www.docker.com/products/docker-desktop/
##### Usa el siguiente comando para levantar los contenedores:
```
docker-compose up --build
```
5. **Ejecuta las migraciones de la base de datos:**
```
dotnet ef database update
```
6. **Ejecuta los tests unitarios**
```
dotnet test
```
7. **Corre la aplicación:**
```
dotnet run
```
**La API estará disponible en http://localhost:5281**



# Endpoints de la API
##### Rutas disponibles
- GET /v1/api/products
- GET /v1/api/products/{id}
- POST /v1/api/products
- PUT /v1/api/products/{id}
- DELETE /v1/api/products/{id}





