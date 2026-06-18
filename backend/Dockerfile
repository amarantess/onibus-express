FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["backend/OnibusExpress.slnx", "backend/"]
COPY ["backend/src/OnibusExpress.Api/OnibusExpress.Api.csproj", "backend/src/OnibusExpress.Api/"]
COPY ["backend/src/OnibusExpress.Application/OnibusExpress.Application.csproj", "backend/src/OnibusExpress.Application/"]
COPY ["backend/src/OnibusExpress.Domain/OnibusExpress.Domain.csproj", "backend/src/OnibusExpress.Domain/"]
COPY ["backend/src/OnibusExpress.Infrastructure/OnibusExpress.Infrastructure.csproj", "backend/src/OnibusExpress.Infrastructure/"]
COPY ["backend/tests/OnibusExpress.Tests/OnibusExpress.Tests.csproj", "backend/tests/OnibusExpress.Tests/"]

RUN dotnet restore "backend/src/OnibusExpress.Api/OnibusExpress.Api.csproj"

COPY backend/ ./backend/

WORKDIR /src/backend/src/OnibusExpress.Api
RUN dotnet publish "OnibusExpress.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OnibusExpress.Api.dll"]
