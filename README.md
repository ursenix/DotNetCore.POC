# DotNetCore.POC

## EF Core Migration Commands:

### Microsoft Doc: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

```
dotnet ef --startup-project "../DotNetCore.API" migrations add InitialCreate
```

```
dotnet ef --startup-project "../DotNetCore.API" migrations script -o “IntialCreate.sql"
```

### From To
```
dotnet ef --startup-project "../DotNetCore.API" migrations script 20170516105557_test1 20170516105316_test
```

### SQL Script file to separate file
```
dotnet ef --startup-project "../DotNetCore.API" migrations script 20170516105557_test1 20170516105316_test -o "reversing migration.sql"
```

### Removing last recent Migration
#### -v verbose
```
dotnet ef --startup-project "../DotNetCore.API" migrations remove -v
```

### Following are the migration scripts:
#### 20170511175515_InitialCreate
#### 20170516121508_TestTableAdded
#### 20170516124540_NewColumnAddedInTest

### Using the following command, the Database will be updated from InitialCreate to TestTableAdded
```
dotnet ef --startup-project "../DotNetCore.API" database update TestTableAdded
```

### Following cmd to generate SQL script for 20170516124540_NewColumnAddedInTest
```
dotnet ef --startup-project "../DotNetCore.API" migrations script "20170516121508_TestTableAdded" "20170516124540_NewColumnAddedInTest"
```
### or simply, execute the following cmd:
```
dotnet ef --startup-project "../DotNetCore.API" database update
```

### Test Table removed
```
dotnet ef --startup-project "../DotNetCore.API" migrations add TestTableRemoved
```

### Following are the migration scripts (updated):
#### 20170511175515_InitialCreate
#### 20170516121508_TestTableAdded
#### 20170516124540_NewColumnAddedInTest
#### 20170516132248_TestTableRemoved

### Generate SQL Script for table deletion alone
```
dotnet ef --startup-project "../DotNetCore.API" migrations script 20170516124540_NewColumnAddedInTest  20170516132248_TestTableRemoved
```

### or simply, execute the following cmd:
```
dotnet ef --startup-project "../DotNetCore.API" database update
```

### To set the column as SQL Identity Column
```
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
```