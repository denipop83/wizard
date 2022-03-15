#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Wizard.Api/Wizard.Api.csproj", "src/Wizard.Api/"]
COPY ["src/Wizard.App/Wizard.App.csproj", "src/Wizard.App/"]
COPY ["src/Wizard.Data/Wizard.Data.csproj", "src/Wizard.Data/"]
COPY ["src/Wizard.Domain/Wizard.Domain.csproj", "src/Wizard.Domain/"]
COPY ["src/Wizard.Utils/Wizard.Utils.csproj", "src/Wizard.Utils/"]
COPY ["src/Wizard.SqliteMigrations/Wizard.SqliteMigrations.csproj", "src/Wizard.SqliteMigrations/"]
COPY ["src/Wizard.PostgresMigrations/Wizard.PostgresMigrations.csproj", "src/Wizard.PostgresMigrations/"]
RUN dotnet restore "src/Wizard.Api/Wizard.Api.csproj"
COPY . .
WORKDIR "/src/src/Wizard.Api"
RUN dotnet build "Wizard.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wizard.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wizard.Api.dll"]