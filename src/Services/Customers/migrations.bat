
IF "%1"=="init-context" dotnet ef migrations add InitialCustomersMigration -o \EInventory.Services.Customer\Shared\Data\Migrations\Customer --project .\EInventory.Services.Customers\EInventory.Services.Customers.csproj -c CustomersDbContext --verbose & goto exit
IF "%1"=="update-context" dotnet ef database update -c CustomersDbContext --verbose --project .\EInventory.Services.Customers\EInventory.Services.Customers.csproj & goto exit 

:exit