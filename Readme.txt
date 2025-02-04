# Ecommerce Crombie

Este repositorio contiene una API desarrollada en .NET que utiliza una base de datos SQL Server, autenticación JWT, y servicios de AWS como Cognito y S3. A continuación, se detallan las configuraciones y dependencias necesarias para ejecutar el proyecto.

## Configuración

### Connection Strings

La cadena de conexión a la base de datos SQL Server se configura en el archivo `appsettings.json` o mediante variables de entorno:

```plaintext
DefaultConnection=
AWS Configuration

La configuración de AWS se utiliza para interactuar con servicios como Cognito y S3. Asegúrate de configurar las siguientes variables de entorno:
plaintext
Copy

AWS_AccessKey=
AWS_SecretKey=
AWS_BucketName=
AWS_Region=
AWS_UserPoolId=
AWS_ClientId=
AWS_ClientSecret=

JWT Configuration

La autenticación JWT se configura con las siguientes claves:
plaintext
Copy

Jwt_Key=
Jwt_Issuer=TuApiUrl
Jwt_Audience=TuApiUrl

Logging

El nivel de logging se configura de la siguiente manera:
plaintext
Copy

Logging_Default=Information
Logging_AspNetCore=Warning

Allowed Hosts

Para permitir cualquier host, se configura:
plaintext
Copy

AllowedHosts=*

Dependencias

El proyecto utiliza las siguientes dependencias de NuGet:

    Amazon.Extensions.CognitoAuthentication (v2.5.5)

    AWSSDK.CognitoIdentityProvider (v3.7.406.2)

    AWSSDK.Extensions.NETCore.Setup (v3.7.301)

    AWSSDK.S3 (v3.7.412.4)

    DotNetEnv (v3.1.1)

    Microsoft.AspNetCore.Authentication.JwtBearer (v8.0.11)

    Microsoft.EntityFrameworkCore (v8.0.11)

    Microsoft.EntityFrameworkCore.Design (v8.0.11)

    Microsoft.EntityFrameworkCore.SqlServer (v8.0.11)

    Microsoft.EntityFrameworkCore.Tools (v8.0.11)

    Microsoft.VisualStudio.Azure.Containers.Tools.Targets (v1.21.0)

    Microsoft.VisualStudio.Web.CodeGeneration.Design (v8.0.7)

    Swashbuckle.AspNetCore (v6.6.2)

Ejecución

Para ejecutar el proyecto, sigue estos pasos:

    Clona el repositorio.

    Configura las variables de entorno mencionadas anteriormente.

    Restaura las dependencias de NuGet.

    Ejecuta las migraciones de Entity Framework para configurar la base de datos.

    Inicia la aplicación.

bash
Copy

dotnet restore
dotnet ef database update
dotnet run


Nota: Asegúrate de no exponer claves de acceso o información sensible en un entorno de producción. Utiliza herramientas como DotNetEnv para manejar configuraciones sensibles de manera segura.
Copy


Este `README.md` proporciona una visión general del proyecto, las configuraciones necesarias, las dependencias, y cómo ejecutar y contribuir al proyecto. Asegúrate de ajustar cualquier detalle específico que pueda ser necesario para tu entorno.

