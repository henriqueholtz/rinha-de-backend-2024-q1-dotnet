# Rinha de Backend 2024-Q1

[Official repository - rinha-de-backend-2024-q1](https://github.com/zanfranceschi/rinha-de-backend-2024-q1)

- :heavy_check_mark: **.NET 8**
- :heavy_check_mark: **[Nginx](https://www.nginx.com/)**
- :heavy_check_mark: **Entity Framework Core**
- :heavy_check_mark: **Docker Support**
- :heavy_check_mark: **Docker-Compose Support**

## Contact

- [LinkedIn](https://www.linkedin.com/in/henrique-holtz/)

---

#### How to run PostgreSQL using docker alone (without docker-compose)

```
docker run --name postgresql-rinha -h postgresql-rinha -p 5432:5432 -e POSTGRES_PASSWORD=Rinh@2024q1 -e POSTGRES_USER=rinha -e POSTGRES_DB=RinhaDb -d postgres:12.18
```

---

#### Migrations

1. Creating the migrations:

```
dotnet ef migrations add "01 - initial migration" --project RinhaBackend.Api/RinhaBackend.Api.csproj
```

2. Applying migrations:

```
dotnet ef database update --project RinhaBackend.Api/RinhaBackend.Api.csproj
```
