FROM mcr.microsoft.com/dotnet/sdk as build-env
WORKDIR /app

# Copy files
COPY . ./

# Restore as a distinct layer
RUN dotnet restore

# Build and publish release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim-arm64v8
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Cream.dll"]
