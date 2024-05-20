FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /src
COPY ./*.sln ./src/App/*.csproj ./

RUN dotnet restore "app-instrumentation.csproj"
COPY . .
WORKDIR "/src/src/App"
RUN dotnet build "app-instrumentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "app-instrumentation.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app-instrumentation.dll"]
