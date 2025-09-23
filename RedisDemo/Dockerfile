# ------------------------
# Build stage
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first (for caching restore)
COPY ["RedisDemo/RedisDemo.csproj", "RedisDemo/"]
RUN dotnet restore "./RedisDemo/RedisDemo.csproj"

# Copy the rest of the source code
COPY ./RedisDemo ./RedisDemo

# Build and publish the app
WORKDIR /src/RedisDemo
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ------------------------
# Runtime stage
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose HTTP (8080) and HTTPS (8081)
EXPOSE 8080
EXPOSE 8081

# Start the application
ENTRYPOINT ["dotnet", "RedisDemo.dll"]
