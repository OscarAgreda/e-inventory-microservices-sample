FROM mcr.microsoft.com/dotnet/sdk:latest as builder

WORKDIR /src

COPY ./.editorconfig ./

COPY ./src/Directory.Build.props ./
COPY ./src/Directory.Build.targets ./
COPY ./src/Packages.props ./
COPY ./src/Services/Orders/Directory.Build.props ./Services/Orders/

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

COPY ./src/Services/Orders/EInventory.Services.Orders/EInventory.Services.Orders.csproj ./Services/Orders/EInventory.Services.Orders/
COPY ./src/Services/Orders/EInventory.Services.Orders.Api/EInventory.Services.Orders.Api.csproj ./Services/Orders/EInventory.Services.Orders.Api/
COPY ./src/Services/Shared/EInventory.Services.Shared/EInventory.Services.Shared.csproj ./Services/Shared/EInventory.Services.Shared/

RUN ls

# restore nuget packages
RUN dotnet restore ./Services/Orders/EInventory.Services.Orders.Api/EInventory.Services.Orders.Api.csproj

# Copy project files
COPY ./src/Ingredients/ ./Ingredients/
COPY ./src/Services/Orders/EInventory.Services.Orders.Api/  ./Services/Orders/EInventory.Services.Orders.Api/
COPY ./src/Services/Orders/EInventory.Services.Orders/  ./Services/Orders/EInventory.Services.Orders/
COPY ./src/Services/Shared/  ./Services/Shared/

# Build project with Release configuration
# and no restore, as we did it already
RUN dotnet build -c Release --no-restore  ./Services/Orders/EInventory.Services.Orders.Api/EInventory.Services.Orders.Api.csproj

WORKDIR /src/Services/Orders/EInventory.Services.Orders.Api/
# Publish project to output folder
# and no build, as we did it already
RUN ls
RUN dotnet publish -c Release --no-build -o out

FROM mcr.microsoft.com/dotnet/aspnet:latest
# Setup working directory for the project
WORKDIR /app
COPY --from=builder /src/Services/Orders/EInventory.Services.Orders.Api/out  .
EXPOSE 80
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "EInventory.Services.Orders.Api.dll"]
