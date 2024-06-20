FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./Directory.Packages.props ./
COPY ./DigitalNotes.API/DigitalNotes.API.csproj DigitalNotes.API/
COPY ./DigitalNotes.Application/DigitalNotes.Application.csproj DigitalNotes.Application/
COPY ./DigitalNotes.Domain/DigitalNotes.Domain.csproj DigitalNotes.Domain/
COPY ./DigitalNotes.Infrastructure/DigitalNotes.Infrastructure.csproj DigitalNotes.Infrastructure/

RUN dotnet restore ./DigitalNotes.API/DigitalNotes.API.csproj
COPY . .
WORKDIR /src/DigitalNotes.API
RUN dotnet build DigitalNotes.API.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish DigitalNotes.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalNotes.API.dll"]