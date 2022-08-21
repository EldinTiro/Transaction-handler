# Transaction-handler
Handling transactions between two different database architectures, in this case, MongoDB and Postgress

<p><em>The main code can be found in Program.cs file.</em></p>

<h2>Notes:</h2>
<p><em>MongoDB only allows transactions via replica-sets, and here are steps how to create replica set instances using docker.</em></p>

<p><code>docker run -p 30001:27017 --name mongo1 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set</code></p>
<p><code>docker run -p 30002:27017 --name mongo2 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set</code></p>
<p><code>docker run -p 30003:27017 --name mongo3 --net my-mongo-cluster mongo:4.4.6 mongod --replSet my-mongo-set</code></p>

<p><em>Connect to one of the intances using MongoDB compass using connection string:</em></p>
<code>mongodb://127.0.0.1:30001/test?directConnection=true</code>
<p><strong>OR</strong></p>
<code>mongodb://127.0.0.1:30002/test?directConnection=true</code>
<p><strong>OR</strong></p>
<code>mongodb://127.0.0.1:30002/test?directConnection=true</code>




