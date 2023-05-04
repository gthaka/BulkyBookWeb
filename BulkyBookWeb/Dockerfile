FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BulkyBookWeb.csproj", "."]
RUN dotnet restore "./BulkyBookWeb.csproj"
COPY . .
WORKDIR "/src/."

RUN dotnet build "BulkyBookWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulkyBookWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
# SQL Server trusted connection problem
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulkyBookWeb.dll"]
