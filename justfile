run:
    docker-compose up

dev-db:
    docker run --rm --name studyTrackerDb \
        -p 5432:5432 \
        -e POSTGRES_USER=admin \
        -e POSTGRES_PASSWORD=admin \
        -e POSTGRES_DB=db \
        postgres:17-alpine

migration-add migrationName:
    dotnet-ef migrations add {{migrationName}} --project=src/Infrastructure/ --startup-project=src/Api/

migrate:
    dotnet-ef database update --project=src/Infrastructure/