
> [!CAUTION]
> This project is made only with the purposal for study Dotnet Aspire, RabbitMq and some clean archtecture concepts, this isn't planned as production ready app.

## Requirements
- .Net 10
- Docker

## Running the project locally
1. Having docker and dotnet 10 installed, just go to AppHost folder `cd AppHost`
2. Execute the application and dotnet aspire will take charge about running and orchestrating the containers `dotnet run`
3. 🔥🔥LFG🔥🔥


> [!INFO]
> Since the application is running at development enviorment, the application will auto apply migrations in the database, so you can just access `https://localhost:7281/scalar/v1` to get the Open API documentation.


## High level archtecture

```mermaid
flowchart LR
    classDef service fill:#1e1e1e,stroke:#555,stroke-width:1px,color:#fff,rx:6px,ry:6px;
    classDef db fill:#0b3d91,stroke:#4b79ff,color:white,rx:6px,ry:6px;
    classDef cache fill:#005f5f,stroke:#1abc9c,color:white,rx:6px,ry:6px;
    classDef queue fill:#5e005e,stroke:#d86fd8,color:white,rx:6px,ry:6px;
    classDef tool fill:#3a3a3a,stroke:#888,color:#fff,rx:6px,ry:6px;

    api[C# API]
    class api service

    cache[Redis Cache:6379]
    class cache cache

    pgsql[(PostgreSQL:5432)]
    class pgsql db

    appdb[(AppDb)]
    class appdb db

    pgadmin[pgAdmin:42061]
    class pgadmin tool

    mongo[(MongoDB:35145)]
    class mongo db

    mongoexpress[Mongo Express:38105]
    class mongoexpress tool

    rabbitmq[RabbitMQ:5672]
    class rabbitmq queue

    api --> cache
    api --> pgsql
    api --> mongo
    api --> rabbitmq

    appdb --> pgsql
    pgadmin --> pgsql
    mongoexpress --> mongo

```

- In this project mongo is used only for store structured logs and nothing more...
- PostgreSQL is the database for storage persistent data, for viewing data the application get an pgAdmin container up
- Redis (Valkey) is the cache layer
- RabbitMQ for async processing using persitent channels