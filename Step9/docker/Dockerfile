# Build stage
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY src/Interface/Interface.csproj ./src/Interface/
RUN dotnet restore src/Interface/Interface.csproj
COPY src/Service/Service.csproj ./src/Service/
RUN dotnet restore src/Service/Service.csproj
COPY src/Client/Client.csproj ./src/Client/
RUN dotnet restore src/Client/Client.csproj
COPY src/Process/Process.csproj ./src/Process/
RUN dotnet restore src/Process/Process.csproj

# Copy Source
COPY . .

# Publish
RUN dotnet publish src/Process/Process.csproj -o /obj

# Runtime stage
FROM microsoft/aspnetcore:2

# set working directory
WORKDIR /app

# Copy compiled binaries
COPY --from=build /obj ./bin

# Copy configuration
COPY config/*.* ./config/

ENV MONGO_ENABLED "true"
ENV MONGO_SERVICE_URI "mongodb://mongo:27017/beacons"
ENV MONGO_COLLECTION "beacons"
#ENV ELASTIC_SEARCH_SERVICE_URI "http://elasticsearch-logging:9200"

EXPOSE 8080

CMD ["dotnet", "./bin/Process.dll", "-c", "./config/config.yml"]
