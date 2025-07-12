# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ValidityControl.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish ValidityControl.csproj -c Release -o /app/publish


# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ValidityControl.dll"]
