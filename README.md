# DBGang.AspNetCore.DataProtection
Implements custom key storage providers

This package stores data protection keys in PostgreSQL database using ADO.NET. To use the package, you need:

1. Create table in your PostgreSQL database: simply run the script in postgresql_table.sql file.
2. Create a database user and grant it SELECT,INSERT permissions to the newly created table.
3. Add the package to your project.
4. Register the service via ConfigureServices in Startup:
```
services.AddDataProtection()
                .PersistKeysToPostgreSQL(YourPostgreSQLConnectionString);
```

