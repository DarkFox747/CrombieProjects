# CrombieProjects

## Ecommerce Crombie

Este repositorio contiene una API desarrollada en .NET que utiliza una base de datos SQL Server, autenticación JWT y servicios de AWS como Cognito y S3. A continuación, se detallan las configuraciones y dependencias necesarias para ejecutar el proyecto.

---

## 📸 Imágenes del Proyecto

### Diagrama de la DB: 

![image](https://github.com/user-attachments/assets/12105db6-e254-4ae5-a733-fb4b26c5efcf)

### Esquema del proyecto:

![image](https://github.com/user-attachments/assets/983f91cc-d8a9-4697-8fc6-b9612b806ffd)


---

## 🔧 Configuración

### 📌 Connection Strings

La cadena de conexión a la base de datos SQL Server se configura en el archivo `appsettings.json` o mediante variables de entorno:

```plaintext
DefaultConnection=tu_cadena_de_conexion
```

### 🌐 AWS Configuration

La configuración de AWS se utiliza para interactuar con servicios como Cognito y S3. Asegúrate de configurar las siguientes variables de entorno:

```plaintext
AWS_AccessKey=
AWS_SecretKey=
AWS_BucketName=
AWS_Region=
AWS_UserPoolId=
AWS_ClientId=
AWS_ClientSecret=
```

### 🔑 JWT Configuration

La autenticación JWT se configura con las siguientes claves:

```plaintext
Jwt_Key=
Jwt_Issuer=TuApiUrl
Jwt_Audience=TuApiUrl
```

### 📋 Logging

El nivel de logging se configura de la siguiente manera:

```plaintext
Logging_Default=Information
Logging_AspNetCore=Warning
```

### 🌍 Allowed Hosts

Para permitir cualquier host, se configura:

```plaintext
AllowedHosts=*
```

---

## 📦 Dependencias

El proyecto utiliza las siguientes dependencias de NuGet:

- **Amazon.Extensions.CognitoAuthentication** (v2.5.5)
- **AWSSDK.CognitoIdentityProvider** (v3.7.406.2)
- **AWSSDK.Extensions.NETCore.Setup** (v3.7.301)
- **AWSSDK.S3** (v3.7.412.4)
- **DotNetEnv** (v3.1.1)
- **Microsoft.AspNetCore.Authentication.JwtBearer** (v8.0.11)
- **Microsoft.EntityFrameworkCore** (v8.0.11)
- **Microsoft.EntityFrameworkCore.Design** (v8.0.11)
- **Microsoft.EntityFrameworkCore.SqlServer** (v8.0.11)
- **Microsoft.EntityFrameworkCore.Tools** (v8.0.11)
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** (v1.21.0)
- **Microsoft.VisualStudio.Web.CodeGeneration.Design** (v8.0.7)
- **Swashbuckle.AspNetCore** (v6.6.2)

---

## 🚀 Ejecución

Para ejecutar el proyecto, sigue estos pasos:

1. Clona el repositorio:

   ```bash
   git clone https://github.com/tu_usuario/CrombieProjects.git
   cd CrombieProjects
   ```

2. Configura las variables de entorno mencionadas anteriormente.

3. Restaura las dependencias de NuGet:

   ```bash
   dotnet restore
   ```

4. Ejecuta las migraciones de Entity Framework para configurar la base de datos:

   ```bash
   dotnet ef database update
   ```

5. Inicia la aplicación:

   ```bash
   dotnet run
   ```

---

## ⚠️ Notas

- Asegúrate de **no exponer claves de acceso o información sensible** en un entorno de producción.
- Utiliza herramientas como **DotNetEnv** para manejar configuraciones sensibles de manera segura.

📌 _Si tienes alguna pregunta o sugerencia, no dudes en contribuir o abrir un issue en el repositorio._

