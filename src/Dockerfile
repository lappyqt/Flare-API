FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["./Flare.sln", "./"]
COPY ["./Flare.Domain/*.csproj", "Flare.Domain/"]
COPY ["./Flare.DataAccess/*.csproj", "Flare.DataAccess/"]
COPY ["./Flare.Application/*.csproj", "Flare.Application/"]
COPY ["./Flare.API/*.csproj", "Flare.API/"]

RUN dotnet restore "./Flare.API/Flare.API.csproj"
COPY . .

WORKDIR "/src/Flare.API"
RUN dotnet build "Flare.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flare.API.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flare.API.dll"]