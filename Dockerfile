FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7113

ENV ASPNETCORE_URLS=http://+:7113

USER app
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
COPY ["src/TaskManager.Api/TaskManager.Api.csproj", "/src/TaskManager.Api/"]
RUN dotnet restore "/src/TaskManager.Api/TaskManager.Api.csproj"
COPY . .
WORKDIR "/src/TaskManager.Api"
RUN dotnet build "TaskManager.Api.csproj" -c $configuration -o /app/build --no-restore

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TaskManager.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]
