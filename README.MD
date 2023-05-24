# WorkHive

## How to run

### Dependencies

- [docker](https://www.docker.com/)
- [docker-compose](https://docs.docker.com/compose/install/)

### How to

Open your favourite termnal and navigate to the root solution folder.
From there run the following command:

`docker compose up --build -d`

### And now?

- The grpc server is exposed at: localhost:8082 in order to call the endpoint you need a grpc client, you can use postman or insomnia, the .proto file can be found in the solution folder at /src/Proto/work_hive_event.proto
- The pgadmin database tool is exposed at: localhost:5050
- The grafana dashboard is exposed at: localhost:3000
- The Kibana log managment tool is exposed at: localhost:5601 