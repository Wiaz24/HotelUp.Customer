FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/HotelUp.Customer.API/HotelUp.Customer.API.csproj", "Src/HotelUp.Customer.API/"]
COPY ["Src/HotelUp.Customer.Application/HotelUp.Customer.Application.csproj", "Src/HotelUp.Customer.Application/"]
COPY ["Src/HotelUp.Customer.Domain/HotelUp.Customer.Domain.csproj", "Src/HotelUp.Customer.Domain/"]
COPY ["Shared/HotelUp.Customer.Shared/HotelUp.Customer.Shared.csproj", "Shared/HotelUp.Customer.Shared/"]
COPY ["Src/HotelUp.Customer.Infrastructure/HotelUp.Customer.Infrastructure.csproj", "Src/HotelUp.Customer.Infrastructure/"]
RUN dotnet restore "Src/HotelUp.Customer.API/HotelUp.Customer.API.csproj"
COPY . .
WORKDIR "/src/Src/HotelUp.Customer.API"
RUN dotnet build "HotelUp.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotelUp.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelUp.Customer.API.dll"]
