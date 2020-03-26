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

## Future Improvements

There's a lot to enhance here.

If you read anything online about event-sourcing patterns, the example that is always used is that of cart management for e-commerce applications.  If you're reasonably confident in your enterprise's chosen messaging backplane, there's no reason to do all the work done by my API synchronously.  You could, for example, do cart product list management using an eventing system.  That might be overkill in some cases, but it might allow for better scalability in others.

I would move cart data management into an interface that is distinct from the CartsController.  If I ever wanted to swap out the persistence provider for the data being served, it would be wise to set up some generic cart acces interface as opposed to making direct calls to the distributed cache interface as I currently am.

Additional work could be done to generalize the PUT operation, and create some sort of Json.Net-based framework for modifications on individual properties of resources.  This is overkill in the case of a single resource.

A PATCH operation could be added with JSON patch syntax (https://tools.ietf.org/html/rfc6902).  This could also later be generalized for re-use.