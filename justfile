run:
    docker-compose up

run-build:
    docker-compose up --build

dev-db:
    docker run --rm \
        --name studyTrackerDb \
        -p 5432:5432 \
        -e POSTGRES_USER=admin \
        -e POSTGRES_PASSWORD=admin \
        -e POSTGRES_DB=db \
        postgres:17-alpine

migration-add migrationName:
    dotnet-ef migrations add {{migrationName}} --project=src/Infrastructure/ --startup-project=src/Api/

migrate:
    just -E=./.env migrate-with-env

migrate-with-env defaultConnection=env("ConnectionStrings__DefaultConnection"): migrate-build
    docker run \
        --network "studytracker_StudyTracker.Postgres.Network" \
        -e ConnectionStrings__DefaultConnection="{{defaultConnection}}" \
        -it studytracker-migrator \
        dotnet-ef database update \
        --project src/Infrastructure/ \
        --startup-project src/Api/ --no-build

migrate-build:
    docker build -f Migrator.Dockerfile . --rm -t studytracker-migrator