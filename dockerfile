FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["disclodo.csproj", "./"]
RUN dotnet restore "./disclodo.csproj"
COPY . .

WORKDIR "/src/."
RUN dotnet build "disclodo.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "disclodo.csproj" -c Release -o /app/publish
FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY certificate.pfx /app/certificate.pfx
ENTRYPOINT ["dotnet", "disclodo.dll"]
 