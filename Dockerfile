# Usa la imagen oficial de .NET SDK para construir
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "CalculadoraAPI.csproj" -c Release -o /app/publish

# Usa la imagen oficial de .NET Runtime para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CalculadoraAPI.dll"]