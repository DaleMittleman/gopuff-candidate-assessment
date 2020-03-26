# Cart Management API
Candidate Assessment for goPuff 

Author: Dale Mittleman (dalemittleman@gmail.com)

## How to Run

I've included in the repository a Dockerfile for the API as well as a docker-compose.yaml file.  If you are inside of the main repo folder and you have the docker-compose tool on your machine, your should be able to run via Docker:

```
docker-compose up --build
```

The compose file brings in a copy of Redis and maps standard redis port to itself.  There are two effects that come out of this decision: 
1) If you have a redis instance on your local machine, this might cause problems.  
2) If you want to run the API without Docker, you can do so against a standard local redis listenin on port 6379

When running via Docker, the API will listen at localhost port 8080.  For a friendly GUI that you can use to both validate that the service is running and play with the API, you can navigate to http://localhost:8080/swagger

## What's There?

There are two sets of endpoints.  One retrieves items from the products list as defined in the products.json file that was included in the original assignment folder.  The other manages the cart.  You can glean what the endpoints do from their HTTP verbs combined with their route structures.

## Technical Choices

I chose to use ASP.NET Core web API to build the API, mostly because this is the technology I'm most comfortable with.  It makes standing up REST services very simple and straightforward.  The fact that the framework leverages DI makes writing the backend almost procedural.

I chose Redis as the backing store.  I'm familiar with the IDistributedCache interface internal to ASP.NET Core.  Redis' impressive qualities as an enterprise distributed cache can be explained by someone wiser than I.  For me, persistence between test runs was a plus, and the ease of use for both local development and docker deployments made it ideal for this project that called for a blank slate on hosting.

Small choice, but I chose snake case serialization because that's how the properties within the products.json file were serialized.

## Future Improvements

There's a lot to enhance here.

### Infrastructure and Code

If you read anything online about event-sourcing patterns, the example that is always used is that of cart management for e-commerce applications.  If you're reasonably confident in your enterprise's chosen messaging backplane, there's no reason to do all the work done by my API synchronously.  You could, for example, do cart product list management using an eventing system.  That might be overkill in some cases, but it might allow for better scalability in others.

I would move cart data management into an interface that is distinct from the CartsController.  If I ever wanted to swap out the persistence provider for the data being served, it would be wise to set up some generic cart acces interface as opposed to making direct calls to the distributed cache interface as I currently am.

In a similar vein, the product list could be pushed on to a backing store, and then edited with a more robust set of API operations.

### API Design

Additional work could be done to generalize the PUT operation, and create some sort of Json.Net-based framework for modifications on individual properties of resources.  This is overkill in the case of a single resource.

A PATCH operation could be added with JSON patch syntax (https://tools.ietf.org/html/rfc6902).  This could also later be generalized for re-use.

It would be nice to have a "Get All Carts" endpoint at GET /Carts, but given the time constraint I didn't want to pipe that out just yet.  The IDistributedCache interface is great for single item sets and sets, but strips Redis of many of its operations on lists, sets, zsets, etc.  If I were move forward with Redis as the store, I'd swap the cache interface out for an isntance of the ConnectionMultiplexer.

### Project Design

I didn't have time to include a tests project, unfortunately.

If I were to move forward with this, I would add 3 more projects.

1) A tests project, containing unit and integration tests.
2) A contracts / DTOs project, containing request / resposne as well as message types.
3) A client project, effectively an SDK for consuming systems

The benefits of having easy-to-use test suites are obvious.  Having a client project and a DTO project allows other internal (or external) teams to easily integrate with your service.  It's good for your team because a client project acts as secondary documentation, and it's good for other teams because they're able to work independently of the original API team without having to spend too much time understand the interface at it's deepest possible level.

Further, if you have a large-scale messaging system, having a DTOs project allows for sharing of message contracts.