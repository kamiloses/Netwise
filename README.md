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



