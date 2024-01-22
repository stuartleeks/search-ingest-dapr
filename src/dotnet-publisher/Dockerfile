FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5264

ENV ASPNETCORE_URLS=http://+:5264


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./ ./
# Restore as distinct layers
RUN dotnet restore ./dotnet-publisher.csproj
# Build and publish a release
RUN dotnet publish ./dotnet-publisher.csproj -c Release -o out

# Build runtime image
FROM base as final
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "dotnet-publisher.dll"]