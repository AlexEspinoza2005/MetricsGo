FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InocuoGoAPI/InocuoGoMetrics.API.csproj", "InocuoGoAPI/"]

RUN dotnet restore "InocuoGoAPI/InocuoGoMetrics.API.csproj"

COPY . .

WORKDIR "/src/InocuoGoAPI"

RUN dotnet build "InocuoGoMetrics.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InocuoGoMetrics.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "InocuoGoMetrics.API.dll"]