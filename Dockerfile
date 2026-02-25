FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "ITIRR.API/ITIRR.API.csproj"
RUN dotnet build "ITIRR.API/ITIRR.API.csproj" -c Release -o /app/build
RUN dotnet publish "ITIRR.API/ITIRR.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ITIRR.API.dll"]