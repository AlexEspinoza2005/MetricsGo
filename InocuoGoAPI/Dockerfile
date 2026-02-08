# Usa la imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copia el archivo de proyecto de la API
COPY ["InocuoGoMetrics.API/InocuoGoMetrics.API.csproj", "InocuoGoMetrics.API/"]
# Restaura dependencias
RUN dotnet restore "InocuoGoMetrics.API/InocuoGoMetrics.API.csproj"
# Copia todo el resto de los archivos
COPY . .
# Se mueve a la carpeta de la API para construirla
WORKDIR "/src/InocuoGoMetrics.API"
RUN dotnet build "InocuoGoMetrics.API.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "InocuoGoMetrics.API.csproj" -c Release -o /app/publish

# Configura la imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "InocuoGoMetrics.API.dll"]