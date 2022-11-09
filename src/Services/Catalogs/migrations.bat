
IF "%1"=="init-context" dotnet ef migrations add InitialCatalogMigration -o \EInventory.Services.Catalogs\Shared\Data\Migrations\Catalogs --project .\EInventory.Services.Catalogs\EInventory.Services.Catalogs.csproj -c CatalogDbContext --verbose & goto exit
IF "%1"=="update-context" dotnet ef database update -c CatalogDbContext --verbose --project .\EInventory.Services.Catalogs\EInventory.Services.Catalogs.csproj & goto exit 

:exit