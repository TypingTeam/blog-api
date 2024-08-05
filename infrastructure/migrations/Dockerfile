FROM mcr.microsoft.com/dotnet/sdk:8.0 AS bundler
USER root
WORKDIR /source

ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef  --version 8.0.7

COPY ["Keeper.sln", "./"]
COPY ["src/Keeper.API/Keeper.API.csproj", "src/Keeper.API/Keeper.API.csproj"]
COPY ["src/Keeper.Application/Keeper.Application.csproj", "src/Keeper.Application/Keeper.Application.csproj"]
COPY ["src/Keeper.Domain/Keeper.Domain.csproj", "src/Keeper.Domain/Keeper.Domain.csproj"]
COPY ["src/Keeper.Infrastructure/Keeper.Infrastructure.csproj", "src/Keeper.Infrastructure/Keeper.Infrastructure.csproj"]
COPY ["tests/Keeper.Tests/Keeper.Tests.csproj", "tests/Keeper.Tests/Keeper.Tests.csproj"]

RUN dotnet restore Keeper.sln

COPY ["src/", "src/"]

RUN dotnet ef migrations bundle --project src/Keeper.Infrastructure/Keeper.Infrastructure.csproj --startup-project src/Keeper.API/Keeper.API.csproj -v --self-contained -r linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS migration
WORKDIR /app
COPY --from=bundler /source/efbundle .
CMD exec ./efbundle --connection $CONNECTION_STRING