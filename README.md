# Instructions for running

## Postgres

Start a `postgres` container listening on `*:5432`:

```
docker run -it -p 5432:5432 postgres
```

## Server

```
cd server
dotnet run
```

Note: The database will be automatically dropped and populated from the `netflix1.csv` file every time you restart the server.

## Client

```
cd client
npm start
```

