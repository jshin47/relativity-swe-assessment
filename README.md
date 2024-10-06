# Instructions for running

## Postgres

Start a `postgres` container listening on `*:5432`:

```
docker run -it -p 5432:5432 postgres
```

## Server

Start an instance of the API Server listening on `localhost:9080`

```
cd server
dotnet run
```

Note: The database will be automatically dropped and populated from the `netflix1.csv` file every time you restart the server.

## Client

Start an instance of the Webpack Server running on `http://localhost:3000`

```
cd client
npm install
npm start
```

