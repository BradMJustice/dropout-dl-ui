#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["dropout-dl-ui/dropout-dl-ui.csproj", "dropout-dl-ui/"]
RUN dotnet restore "dropout-dl-ui/dropout-dl-ui.csproj"
COPY . .
WORKDIR "/src/dropout-dl-ui"
RUN dotnet build "dropout-dl-ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dropout-dl-ui.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dropout-dl-ui.dll"]