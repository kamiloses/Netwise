# Netwise – Cat Fact Logger API

A simple .NET Web API that fetches random cat facts from an external API and stores them in a local `.txt` file.

## Features

- Connects to `https://catfact.ninja/fact`
- Appends each fetched fact to a local `catfacts.txt` file (one per request)
- Designed using:
  - ASP.NET Core Web API
  - Dependency Injection
  - Custom Middleware for exception handling
  - Logging
  - Configuration via `appsettings.json`
- Includes unit and integration tests

---


# How to run the application locally

1. Clone the repository:

    ````bash
    git clone https://github.com/kamiloses/Netwise.git
    ````

2. Navigate to the project folder:

    ````bash
    cd Netwise
    ````

3. Check tests (optional):

    ````bash
    cd Netwise.Tests
    dotnet test
    cd ..
    ````

4. Run the application:

    ````bash
    cd Netwise
    start dotnet run
    ````

5. Send a request to the API:

    ````
    GET http://localhost:8080/api/catfact
    ````

6. Check if the cat facts file was created and display its content:

    ````bash
    type catfacts.txt
    ````

<img width="1660" height="746" alt="image" src="https://github.com/user-attachments/assets/971d1708-b93b-4598-a0d4-6b8e2ae4fc9e" />


