#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HamsterApi.Api/HamsterApi.Api.csproj", "HamsterApi.Api/"]
COPY ["HamsterApi.Application/HamsterApi.Application.csproj", "HamsterApi.Application/"]
COPY ["HamsterApi.Domain/HamsterApi.Domain.csproj", "HamsterApi.Domain/"]
COPY ["HamsterApi.Infrastructure/HamsterApi.Infrastructure.csproj", "HamsterApi.Infrastructure/"]
COPY ["HamsterApi.Persistence/HamsterApi.Persistence.csproj", "HamsterApi.Persistence/"]
RUN dotnet restore "./HamsterApi.Api/./HamsterApi.Api.csproj"
COPY . .
WORKDIR "/src/HamsterApi.Api"
RUN dotnet build "./HamsterApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HamsterApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HamsterApi.Api.dll"]