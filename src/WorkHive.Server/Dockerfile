#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WorkHive.Server/WorkHive.Server.csproj", "src/WorkHive.Server/"]
COPY ["src/WorkHive.Infrastructure/WorkHive.Infrastructure.csproj", "src/WorkHive.Infrastructure/"]
COPY ["src/WorkHive.Application/WorkHive.Application.csproj", "src/WorkHive.Application/"]
COPY ["src/WorkHive.Domain/WorkHive.Domain.csproj", "src/WorkHive.Domain/"]
RUN dotnet restore "src/WorkHive.Server/WorkHive.Server.csproj"
COPY . .
WORKDIR "/src/src/WorkHive.Server"
RUN dotnet build "WorkHive.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkHive.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkHive.Server.dll"]