# Movies Microservice
**Version: 1.0**
## Scenario

This project is a movies indexing application, which has the following functionality:

- **Movie detail**
  - Display selected movie detail information
- **Create Movie**
  - Create a new movie that can be retrieved in the movies list
- **Update Movie**
  - Update movies data.

A `movies.json` file with some mock data is provided to help with testing.

### Running the application

- Make sure the startup project is set to `Movies.Server`
- AWS DynamoDB is used as a storage provider. Locally, run `docker-compose up` command to start up localstack with DynamoDB
- The project has one controller `MoviesController` that serves the following requests:
  - [GET] http://localhost:6600/movies/{id}
  - [POST] http://localhost:6600/movies
  - [PUT] http://localhost:6600/movies/{id}

### Request example

```
{
"id": 1,
"key": "deadpool",
"name": "Deadpool",
"description": "A former Special Forces operative turned mercenary is subjected to a rogue experiment that leaves him with accelerated healing powers, adopting the alter ego Deadpool.",
"genres": 
  [
    "action",
    "adventure",
    "comedy"
  ],
"rate": "8.6",
"length": "1hr 48mins",
"img": "deadpool.jpg"
}
```

### Improvements

- Add functionality to list all movies
- Add functionality to list top 5 rated movies
- Add functionality to search and filter by genre using GraphQL
- Pre-load DynamoDB with data on start up for faster retrieval
- Extend unit tests to cover all functionality
- Add end-to-end tests to test flows
