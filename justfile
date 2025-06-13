db-run:
    docker run --rm --name studyTrackerDb \
        -p 5432:5432 \
        -e POSTGRES_USER=admin \
        -e POSTGRES_PASSWORD=admin \
        -e POSTGRES_DB=db \
        postgres:17-alpine

migration-run migrationName:
    dotnet-ef migrations add {{migrationName}} --project=src/Infrastructure/ --startup-project=src/Api/

upd-db:
    dotnet-ef database update --project=src/Api/