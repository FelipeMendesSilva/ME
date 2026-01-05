# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para dentro do cont�iner
COPY . .

# Restaura e publica o projeto principal
RUN dotnet restore "ME.SuperHero.API/ME.SuperHero.API.csproj"
RUN dotnet publish "ME.SuperHero.API/ME.SuperHero.API.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Definindo o ambiente
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish .

EXPOSE 7184
ENTRYPOINT ["dotnet", "ME.SuperHero.API.dll"]