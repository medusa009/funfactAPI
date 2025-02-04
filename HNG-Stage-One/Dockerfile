FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HNG-Stage-One/HNG-Stage-One.csproj", "HNG-Stage-One/"]
RUN dotnet restore "HNG-Stage-One/HNG-Stage-One.csproj"
COPY . .
WORKDIR "/src/HNG-Stage-One"
RUN dotnet build "HNG-Stage-One.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HNG-Stage-One.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HNG-Stage-One.dll"]
