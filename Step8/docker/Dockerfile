# Build stage
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY Source/Interface/Interface.csproj ./Source/Interface/
RUN dotnet restore Source/Interface/Interface.csproj
COPY Source/Service/Service.csproj ./Source/Service/
RUN dotnet restore Source/Service/Service.csproj
COPY Source/Client/Client.csproj ./Source/Client/
RUN dotnet restore Source/Client/Client.csproj
COPY Source/Process/Process.csproj ./Source/Process/
RUN dotnet restore Source/Process/Process.csproj

# Copy Source
COPY . .

# Test
ENV TEAMCITY_PROJECT_NAME=fake
#RUN dotnet test tests/Service.Test/Service.Test.csproj

# Publish
RUN dotnet publish Source/Process/Process.csproj -o /obj

# Runtime stage
FROM microsoft/aspnetcore:2

# set working directory
WORKDIR /app

# Copy compiled binaries
COPY --from=build /obj ./bin

# Copy configuration
COPY config/*.* ./config/

ENV MONGO_ENABLED "true"
ENV MONGO_SERVICE_URI "mongodb://localhost:27017/beacons"
ENV MONGO_COLLECTION "beacons"
#ENV ELASTIC_SEARCH_SERVICE_URI "http://elasticsearch-logging:9200"

EXPOSE 8080

CMD ["dotnet", "./bin/Process.dll", "-c", "./config/config.yml"]
