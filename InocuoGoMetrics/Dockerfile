# Usa la imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copia el archivo de proyecto de la Web
COPY ["InocuoGoMetrics/InocuoGoMetrics.csproj", "InocuoGoMetrics/"]
# Restaura dependencias (asegúrate de que los DTOs estén disponibles o copiados si están en otra carpeta,
# pero con la estructura actual esto debería funcionar si están en el mismo proyecto o referenciados correctamente)
RUN dotnet restore "InocuoGoMetrics/InocuoGoMetrics.csproj"
# Copia todo el resto
COPY . .
# Se mueve a la carpeta de la Web
WORKDIR "/src/InocuoGoMetrics"
RUN dotnet build "InocuoGoMetrics.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "InocuoGoMetrics.csproj" -c Release -o /app/publish

# Configura la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "InocuoGoMetrics.dll"]