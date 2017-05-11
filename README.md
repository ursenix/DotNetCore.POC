# DotNetCore.POC

# EF Core Migration Commands:

dotnet ef --startup-project "../DotNetCore.API" migrations add InitialCreate

dotnet ef --startup-project "../DotNetCore.API" migrations script -o “IntialCreate.sql"
