FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StammPhoenix.Persistence/StammPhoenix.Persistence.csproj", "StammPhoenix.Persistence/"]
COPY ["StammPhoenix.Web/StammPhoenix.Web.csproj", "StammPhoenix.Web/"]
RUN dotnet restore "StammPhoenix.Web/StammPhoenix.Web.csproj"
COPY . .
WORKDIR "/src/StammPhoenix.Web"
RUN dotnet build "StammPhoenix.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StammPhoenix.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StammPhoenix.Web.dll"]
