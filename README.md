# Transaction-handler
Handling transactions between two different database architectures, in this case, MongoDB and Postgress

The main code can be found in Program.cs file.



Notes:
MongoDB only allows transactions via replica-sets, and here are steps how to create replica set instances using docker.

docker run -p 30001:27017 --name mongo1 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set
docker run -p 30002:27017 --name mongo2 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set
docker run -p 30003:27017 --name mongo3 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set

Connect to one of the intances using MongoDB compass using connection string:
mongodb://127.0.0.1:30001/test?directConnection=true
OR
mongodb://127.0.0.1:30002/test?directConnection=true
OR
mongodb://127.0.0.1:30002/test?directConnection=true
