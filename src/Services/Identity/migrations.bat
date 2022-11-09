
IF "%1"=="init-context" dotnet ef migrations add InitialIdentityServerMigration -o \EInventory.Services.Identity\Shared\Data\Migrations\Identity --project .\EInventory.Services.Identity\EInventory.Services.Identity.csproj -c IdentityContext --verbose & goto exit
IF "%1"=="update-context" dotnet ef database update -c IdentityContext --verbose --project .\EInventory.Services.Identity\EInventory.Services.Identity.csproj & goto exit 
:exit