# cribl-log-collector

## Log Collector Service
At its core, the Log Collector Service provides a REST interface for clients to query Unix machines for their logs files.

The service supports filtering the logs returned by number of events and a simple keyword search.
[Github Repository](https://github.com/brianfranko/cribl-log-collector)

### Getting Started
#### Run the Log Collector Service Locally
- Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
- Clone the Log Collector repository
- Open the repository in Visual Studio or Rider
- Set the LogCollector project as the startup project
- Build and Run/Debug the solution
- Open a browser window to http://localhost:7005/swagger/index.html to access the local Swagger API
#### Run the Log Collector Service in Docker Locally
- Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
- Clone the Log Collector repository
- Open the repository in Visual Studio or Rider
- Build the docker image with `docker-compose build LogCollector`
- Run the docker image with `docker-compose up LogCollector`
- Open a browser window to http://localhost:8080/swagger/index.html
### Testing framework information
- [Unit Tests](https://github.com/brianfranko/cribl-log-collector/tree/main/LogCollector/LogCollector.UnitTests)