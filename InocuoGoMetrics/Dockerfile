FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InocuoGoMetrics/InocuoGoMetrics.csproj", "InocuoGoMetrics/"]
RUN dotnet restore "InocuoGoMetrics/InocuoGoMetrics.csproj"

COPY . .
WORKDIR "/src/InocuoGoMetrics"
RUN dotnet build "InocuoGoMetrics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InocuoGoMetrics.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "InocuoGoMetrics.dll"]