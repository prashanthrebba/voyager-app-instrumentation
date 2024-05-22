FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

ENV OTEL_EXPORTER_OTLP_ENDPOINT=https://otlp.nr-data.net:4317
ENV OTEL_EXPORTER_OTLP_HEADERS=api-key=52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL
# ENV OTEL_ATTRIBUTE_VALUE_LENGTH_LIMIT=4095
# ENV OTEL_SERVICE_NAME=Voyager.Api
# ENV OTEL_RESOURCE_ATTRIBUTES=service.instance.id=1

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /src
COPY ./*.sln ./src/Voyager.Api/*.csproj ./

RUN dotnet restore "Voyager.Api.csproj"
COPY . .
WORKDIR "/src/src/Voyager.Api"
RUN dotnet build "Voyager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Voyager.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Voyager.Api.dll"]
