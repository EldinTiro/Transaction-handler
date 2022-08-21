// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Driver;
using Npgsql;
using System.Transactions;

Console.WriteLine("Postgres - MongoDB chained transaction demo");

try
{
    string postgressConnectionString = "Host=localhost; Username = pony; Password = Password1; Database = delivery_platform_clients";
    string mongoDBConnectionString = "mongodb://127.0.0.1:30001/test?directConnection=true";

    var client = new MongoClient(mongoDBConnectionString);
    var database1 = client.GetDatabase("AdressBook");
    var collection1 = database1.GetCollection<BsonDocument>("foo").WithWriteConcern(WriteConcern.WMajority);
    using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    {
        //Postgres
        using (NpgsqlConnection postgresConnection = new NpgsqlConnection(postgressConnectionString))
        {
            postgresConnection.Open();

            NpgsqlCommand postgressCmd = new NpgsqlCommand(@"CREATE TABLE table11(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)", postgresConnection);
            NpgsqlCommand postgressCmd2 = new NpgsqlCommand(@"CREATE TABLE table12(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)", postgresConnection);
            postgressCmd.ExecuteNonQuery();
            postgressCmd2.ExecuteNonQuery();

            Console.WriteLine("No errors on the postgres side");

            //MongoDB
            using (var mongoSession = client.StartSession())
            {
                var cancellationToken = CancellationToken.None;

                var sameDocument = new BsonDocument("Insert1", 10);
                var sameDocument2 = new BsonDocument("Insert2", 20);

                var result = mongoSession.WithTransaction(
                    (s, ct) =>
                    {
                        collection1.InsertOne(s, sameDocument, cancellationToken: ct);
                        collection1.InsertOne(s, sameDocument2, cancellationToken: ct);
                        return "Inserted into collections in different databases";
                    },
                    new MongoDB.Driver.TransactionOptions(),
                    cancellationToken);

                Console.WriteLine("No errors on the mongodb side");
            }
        }

        scope.Complete();
    }
}
catch (TransactionAbortedException ex)
{
    Console.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
}