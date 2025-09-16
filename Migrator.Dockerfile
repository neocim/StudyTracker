FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY src/Application/Application.csproj src/Application/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/

RUN dotnet restore src/Infrastructure/Infrastructure.csproj

COPY src/Application src/Application/
COPY src/Domain src/Domain/
COPY src/Infrastructure src/Infrastructure/

RUN dotnet publish src/Infrastructure/Infrastructure.csproj -c Release --output publish/

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY --from=build /app/publish ./