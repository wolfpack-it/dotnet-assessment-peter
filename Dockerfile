#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Wolfpack.Api/Wolfpack.Api.csproj", "Wolfpack.Api/"]
RUN dotnet restore "Wolfpack.Api/Wolfpack.Api.csproj"
COPY . .
WORKDIR "/src/Wolfpack.Api"
RUN dotnet build "Wolfpack.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wolfpack.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wolfpack.Api.dll"]