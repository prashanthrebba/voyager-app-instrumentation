FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

ENV OTEL_EXPORTER_OTLP_ENDPOINT=https://otlp.nr-data.net
ENV OTEL_EXPORTER_OTLP_HEADERS=api-key=52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL
ENV OTEL_ATTRIBUTE_VALUE_LENGTH_LIMIT=4095
ENV OTEL_SERVICE_NAME=app-instrumentation
ENV OTEL_RESOURCE_ATTRIBUTES=service.instance.id=1

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
