FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app

COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure

RUN dotnet restore src/Infrastructure/Infrastructure.csproj

COPY ./src/Infrastructure ./src/Infrastructure

RUN dotnet publish src/Infrastructure/Infrastructure.csproj -c Release --output publish/ --no-restore

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./