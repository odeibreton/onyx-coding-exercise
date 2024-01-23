# Onyx coding exercise

## Introduction
This solution is built using .NET 8, ASP.NET Core and Domain-Driven Design (DDD) principles. The solution is split into 3 projects located in the `src` folder:
- Domain
- Infrastructure
- API

Each project above has a corresponding test project in the `test` folder.

## Domain
This project contains the domain model and business logic. This solution did not require a big domain model, neither any complex logic, so it's very simple.

The main and only entity in the domain is the `Product` entity. It contains properties to store the ID, name and colour.

All properties are immutable and can only be set through the constructor. The `Name` property is validated to ensure it's not null or empty.

The colour property is an enum (`ProductColour`) with the following values:
- Red
- Blue
- Green

## Infrastructure
The infrastructure project contains the code to retrieve products from the persistence layer. In this case, the persistence layer is an in-memory array of products defined by configuring the repository.

## API
The API layer is the most complex layer in this project. The API can be split into 3 main parts:
- Authentication and Authorisation
- Endpoints (Controllers)
- Mapping

### Authentication and Authorisation
The specification document did not speficy which kind of authentication should be used, so I decided to implement a simple API key authentication.

> [!CAUTION]
> This implementation of API key authentication is not intended to be production-ready.
> It's just a simple implementation to allow me to demonstrate authentication without having to deal with JWT tokens or cookies.

The authentication section can be configured to use a different API key by changing the `ApiKey` value in the `appsettings.json` file.
It also allows to configure which header to use to get the API key. The default value is `X-Api-Key`.

The authentication will only succeed if the API key in the request header matches the one configured in the `appsettings.json` file.

The `ApiKeyAuthenticationOptions` contain some validation to ensure that the API key and the header name are not null or empty. If they are, the API will throw an exception.

The authentication is configured in the `Program.cs` file.

### Endpoints (Controllers)
The API three endpoints:
- **Health check:** GET /
- **Get products:** GET /products
- **Get products by colour:** GET /products/query?colour={colour}

#### Health check
The health check uses the ASP.NET Core health check services and middleware.
It is configured to allow anonymous access.

#### Get products
This endpoint returns all products in the repository.

It is configured to require authorisation using the default configured schema, in this case, the API key authentication.

#### Get products by colour
This endpoint returns all products in the repository that match the colour specified in the query string.

It is configured to require authorisation using the default configured schema, in this case, the API key authentication.

I designed this endpoint using query parameters having in mind that this endpoint could be extended to allow filtering by other properties.

> [!NOTE]
> The controllers contain logic to map the request, retrieve the data and map the response.
> I would move this logic to a separate service, and I would only make the controller responsible for returning the correct response type (200 OK, 400 Not Found, etc...).

### Mapping
I added a very simple mapping section to map to and from the domain model and the API model.

I tend to not use mapping libraries like AutoMapper because I find them to be too complex for simple mapping scenarios like this one.

## Tests
In this section I will explain the tests I have written for the API layer. The tests in the other two layers are very simple, and I don't think require much explanation.

The API tests are composed by a fixture class, unit tests for the authentication logic and integration tests for the endpoints.

### Fixture
The fixture class contains the logic to create a test server and a HTTP client to be used by the tests.

It exposes two HTTP clients, one with the API key header and one without it. This allows to test the endpoints with and without authentication.

### Authentication tests
These unit tests make sure that the authentication logic works as expected.

I use `NSubstitute` to mock some of the ASP.NET Core dependencies that are difficult to work with.

### Endpoints tests
The integration tests are divided by authentication.

The tests that make sure authentication is being enforced for the endpoints use the HTTP client without the API key header, and expect a 401 Unauthorized response.

The tests that make sure the endpoints return the correct response use the HTTP client with the API key header.
