FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Crear el directorio para las imágenes
RUN mkdir -p /app/Images/Lessons

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/MEJORA.Api/MEJORA.Api.csproj", "src/MEJORA.Api/"]
COPY ["src/MEJORA.Application.UseCase/MEJORA.Application.UseCase.csproj", "src/MEJORA.Application.UseCase/"]
COPY ["src/MEJORA.Application.Dtos/MEJORA.Application.Dtos.csproj", "src/MEJORA.Application.Dtos/"]
COPY ["src/MEJORA.Application.Interface/MEJORA.Application.Interface.csproj", "src/MEJORA.Application.Interface/"]
COPY ["src/MEJORA.Infrastructure/MEJORA.Infrastructure.csproj", "src/MEJORA.Infrastructure/"]
COPY ["src/MEJORA.Utilities/MEJORA.Utilities.csproj", "src/MEJORA.Utilities/"]
RUN dotnet restore "src/MEJORA.Api/MEJORA.Api.csproj"
COPY . .
WORKDIR "/src/src/MEJORA.Api"
RUN dotnet build "MEJORA.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MEJORA.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Crear el directorio de imágenes en el contenedor final
RUN mkdir -p /app/Images/Lessons

ENTRYPOINT ["dotnet", "MEJORA.Api.dll"]
