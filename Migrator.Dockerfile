FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY src/Api/Api.csproj src/Api/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/

COPY src/Api ./src/Api/
COPY src/Infrastructure ./src/Infrastructure/

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY --from=build /app ./