#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Asset.Booking.API/Asset.Booking.API.csproj", "Asset.Booking.API/"]
RUN dotnet restore "Asset.Booking.API/Asset.Booking.API.csproj"
COPY . .
WORKDIR "/src/Asset.Booking.API"
RUN dotnet build "Asset.Booking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asset.Booking.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asset.Booking.API.dll"]