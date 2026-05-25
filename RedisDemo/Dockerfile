# =========================================================
# Generic .NET 8 Microservice Dockerfile
# Reusable for multiple microservices
# =========================================================

# ------------------------
# Build Stage
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# ---------- Build Arguments ----------
ARG PROJECT_NAME=RedisDemo
ARG PROJECT_PATH=./src/RedisDemo
ARG BUILD_CONFIGURATION=Develop

WORKDIR /src

# Copy csproj first for Docker layer caching
COPY ${PROJECT_PATH}/${PROJECT_NAME}.csproj ${PROJECT_PATH}/

# Restore dependencies
RUN dotnet restore ${PROJECT_PATH}/${PROJECT_NAME}.csproj

# Copy remaining source code
COPY . .

# Move into project folder
WORKDIR /src/${PROJECT_PATH}

# Publish application
RUN dotnet publish -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# ------------------------
# Runtime Stage
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# ---------- Runtime Arguments ----------
ARG PROJECT_NAME=RedisDemo

WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Start app
ENTRYPOINT ["sh", "-c", "dotnet ${PROJECT_NAME}.dll"]