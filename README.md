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

Note: The database will be automatically dropped and populated from the `netflix1.csv` file every time you restart the server. This operation can take a minute or so.

## Client

Start an instance of the Webpack Server running on `http://localhost:3000`

```
cd client
npm install
npm start
```

# Server Notes

## Libraries Used

### `efcore` and Postgresql driver for it

I chose to use `efcore` to demonstrate that I know what Entity Framework is and have a basic understanding of how to use it. I considered choosing another, simpler ORM like Dapper, but I ended up going with EF in the interest of demonstrating awareness of it.

### `CSVHelper`

For parsing the `netflix1.csv` file.

### `AutoMapper`

For mapping classes to classes by convention (typically, DTO classes to and from each other or Entities)

# Client Notes

## Libraries Used

### `react-bs-datatable` for data table listing shows

I used `react-bs-datatable` because it already implemented client-side paging, sorting, and filtering. The `DatatableWrapper` component in `clients/src/components/ListShowComponent.jsx` includes configuration under the `headers` prop for which columns are displayed.

### `react-select` for autocomplete inputs

I used `react-select` `CreatableSelect` component to implement autocomplete and multi-select on the following inputs in `client/src/components/CreateShowComponent.jsx`:

* Rating
* Country
* Categories

### `react-datepicker` for Date Added

I used `react-datepicker` `DatePicker` component to implement a date picker for the "Date Added" input.