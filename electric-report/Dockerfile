FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["electric-report/electric-report.csproj", "electric-report/"]
RUN dotnet restore "electric-report/electric-report.csproj"
COPY . .
WORKDIR "/src/electric-report"
RUN dotnet build "electric-report.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "electric-report.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "electric-report.dll"]
