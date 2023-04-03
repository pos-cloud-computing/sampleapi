FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web.API/Web.API.csproj", "Web.API/"]
COPY ["Application.DTO/Application.DTO.csproj", "Application.DTO/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application.Facade/Application.Facade.csproj", "Application.Facade/"]
COPY ["Infra.Repository/Infra.Repository.csproj", "Infra.Repository/"]
RUN dotnet restore "Web.API/Web.API.csproj"
COPY . .
WORKDIR "/src/Web.API"
RUN dotnet build "Web.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["dotnet", "Web.API.dll"]