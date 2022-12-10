## Start a local dev database with docker

```bash
docker run -d --name disclodo-postgres -e POSTGRES_PASSWORD=pgpassword -e POSTGRES_USER=pguser -e POSTGRES_DB=pgdb -v docker_database:/var/lib/postgresql/data -p 5432:5432 postgres:15
```

## Entity framework core migration

Set database generated default value for models attributes by adding a sql statement to the `OnModelCreating` method of the `DataContext` class

```csharp
modelBuilder.Entity<User>().Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
```

Create migration from the models

```bash
dotnet ef migrations add "Name of migration"
```

Update Databse with created migration

```
dotnet ef database update
```
