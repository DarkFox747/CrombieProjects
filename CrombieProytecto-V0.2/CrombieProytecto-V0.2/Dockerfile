# Fase base para ejecutar el contenedor
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CrombieProytecto-V0.2.csproj", "."]
RUN dotnet restore "./CrombieProytecto-V0.2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CrombieProytecto-V0.2.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CrombieProytecto-V0.2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para producción
FROM base AS final
WORKDIR /app

# Copiar la publicación desde la fase anterior
COPY --from=publish /app/publish .

# Copiar el archivo .env al contenedor
COPY /*.env .

# Configurar las variables de entorno desde el archivo .env
ENV DOTNET_ENVIRONMENT=Production

# Comando para ejecutar el servicio
ENTRYPOINT ["dotnet", "CrombieProytecto-V0.2.dll"]
