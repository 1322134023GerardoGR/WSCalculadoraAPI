# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia SOLO el archivo .csproj y restaura dependencias
COPY *.csproj .
RUN dotnet restore

# Copia el resto y publica
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WSCalculadoraAPI.dll"]