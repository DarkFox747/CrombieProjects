# Fase base para ejecutar el contenedor
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar los archivos de la solución y los proyectos
COPY ["Proyect_Models/Proyect_Models.csproj", "Proyect_Models/"]
COPY ["Proyect_DAL/Proyect_DAL.csproj", "Proyect_DAL/"]
COPY ["Proyect_BLL/Proyect_BLL.csproj", "Proyect_BLL/"]
COPY ["CrombieProytecto-V0.2/CrombieProytecto-V0.2.csproj", "CrombieProytecto-V0.2/"]

RUN dotnet restore "CrombieProytecto-V0.2/CrombieProytecto-V0.2.csproj"

# Copiar el resto del código fuente
COPY . .

WORKDIR "/src/CrombieProytecto-V0.2"

RUN dotnet build "./CrombieProytecto-V0.2.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CrombieProytecto-V0.2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para producción
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

# Copiar el archivo .env si existe
COPY /*.env . 

# Configurar variables de entorno
ENV DOTNET_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CrombieProytecto-V0.2.dll"]
