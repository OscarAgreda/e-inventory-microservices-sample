FROM mcr.microsoft.com/dotnet/sdk:latest as builder

WORKDIR /src

COPY ./.editorconfig ./

COPY ./src/Directory.Build.props ./
COPY ./src/Directory.Build.targets ./
COPY ./src/Packages.props ./
COPY ./src/Services/Customers/Directory.Build.props ./Services/Customers/

# TODO: Using wildcard to copy all files in the directory.
COPY ./src/Ingredients/Ingredients.Abstractions/Ingredients.Abstractions.csproj ./Ingredients/Ingredients.Abstractions/
COPY ./src/Ingredients/Ingredients.Core/Ingredients.Core.csproj ./Ingredients/Ingredients.Core/
COPY ./src/Ingredients/Ingredients.Caching.InMemory/Ingredients.Caching.InMemory.csproj ./Ingredients/Ingredients.Caching.InMemory/
COPY ./src/Ingredients/Ingredients.Email/Ingredients.Email.csproj ./Ingredients/Ingredients.Email/
COPY ./src/Ingredients/Ingredients.Integration.MassTransit/Ingredients.Integration.MassTransit.csproj ./Ingredients/Ingredients.Integration.MassTransit/
COPY ./src/Ingredients/Ingredients.Logging/Ingredients.Logging.csproj ./Ingredients/Ingredients.Logging/
COPY ./src/Ingredients/Ingredients.Monitoring/Ingredients.Monitoring.csproj ./Ingredients/Ingredients.Monitoring/
COPY ./src/Ingredients/Ingredients.Persistence.EfCore.Postgres/Ingredients.Persistence.EfCore.Postgres.csproj ./Ingredients/Ingredients.Persistence.EfCore.Postgres/
COPY ./src/Ingredients/Ingredients.Persistence.Mongo/Ingredients.Persistence.Mongo.csproj ./Ingredients/Ingredients.Persistence.Mongo/
COPY ./src/Ingredients/Ingredients.Resiliency/Ingredients.Resiliency.csproj ./Ingredients/Ingredients.Resiliency/
COPY ./src/Ingredients/Ingredients.Security/Ingredients.Security.csproj ./Ingredients/Ingredients.Security/
COPY ./src/Ingredients/Ingredients.Swagger/Ingredients.Swagger.csproj ./Ingredients/Ingredients.Swagger/
COPY ./src/Ingredients/Ingredients.Validation/Ingredients.Validation.csproj ./Ingredients/Ingredients.Validation/
COPY ./src/Ingredients/Ingredients.Web/Ingredients.Web.csproj ./Ingredients/Ingredients.Web/

COPY ./src/Services/Customers/EInventory.Services.Customers/EInventory.Services.Customers.csproj ./Services/Customers/EInventory.Services.Customers/
COPY ./src/Services/Customers/EInventory.Services.Customers.Api/EInventory.Services.Customers.Api.csproj ./Services/Customers/EInventory.Services.Customers.Api/
COPY ./src/Services/Shared/EInventory.Services.Shared/EInventory.Services.Shared.csproj ./Services/Shared/EInventory.Services.Shared/

RUN ls

# restore nuget packages
RUN dotnet restore ./Services/Customers/EInventory.Services.Customers.Api/EInventory.Services.Customers.Api.csproj

# Copy project files
COPY ./src/Ingredients/ ./Ingredients/
COPY ./src/Services/Customers/EInventory.Services.Customers.Api/  ./Services/Customers/EInventory.Services.Customers.Api/
COPY ./src/Services/Customers/EInventory.Services.Customers/  ./Services/Customers/EInventory.Services.Customers/
COPY ./src/Services/Shared/  ./Services/Shared/

# Build project with Release configuration
# and no restore, as we did it already
RUN dotnet build -c Release --no-restore  ./Services/Customers/EInventory.Services.Customers.Api/EInventory.Services.Customers.Api.csproj

WORKDIR /src/Services/Customers/EInventory.Services.Customers.Api/
# Publish project to output folder
# and no build, as we did it already
RUN ls
RUN dotnet publish -c Release --no-build -o out

FROM mcr.microsoft.com/dotnet/aspnet:latest
# Setup working directory for the project
WORKDIR /app
COPY --from=builder /src/Services/Customers/EInventory.Services.Customers.Api/out  .
EXPOSE 80
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "EInventory.Services.Customers.Api.dll"]
