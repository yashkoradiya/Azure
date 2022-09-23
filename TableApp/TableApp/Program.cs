using Azure;

using Azure.Data.Tables;

using Azure.Data.Tables.Models;

using System.Collections.Concurrent;



var serviceClient = new TableServiceClient(

    new Uri("https://sa22sep.table.core.windows.net"),

    new TableSharedKeyCredential("sa22sep", "JpOi7lplh0rBPKegBf4FoZnvzioGrojstsONqjJvF51tgIoPzJuEXe1Cb/el0bD7NJaj3a7nCq3v+AStG/E0/g=="));

string tableName = "OfficeSupplies";



TableItem table = serviceClient.CreateTableIfNotExists(tableName);

Console.WriteLine($"The created table's name is {table.Name}.");



//Create a new table.The TableItem class stores properties of the created table.

Pageable<TableItem> queryTableResults = serviceClient.Query(filter: $"TableName eq '{tableName}'");

Console.WriteLine("The following are the names of the tables in the query results:");



// Iterate the <see cref="Pageable"> in order to access queried tables.

foreach (TableItem t in queryTableResults)

{

    Console.WriteLine(t.Name);

}



// Deletes the table made previously.

//serviceClient.DeleteTable(tableName);

//Console.WriteLine("Deleted");



//CREATE A NEW TABLE OF SAME NAME TO TEST TableClient CLASS

//Construct a new < see cref = "TableClient" /> using a <see cref="TableSharedKeyCredential" />.

var tableClient = new TableClient(

    new Uri("https://sa22sep.table.core.windows.net"),

    "OfficeSupplies1p1",

new TableSharedKeyCredential("sa22sep", "JpOi7lplh0rBPKegBf4FoZnvzioGrojstsONqjJvF51tgIoPzJuEXe1Cb/el0bD7NJaj3a7nCq3v+AStG/E0/g=="));



// Create the table in the service.

tableClient.Create();



// Make a dictionary entity by defining a <see cref="TableEntity">.

var entity = new TableEntity("partitionKey", "rowKey")

{

    { "Product", "Marker Set" },

    { "Price", 50.00 },

    { "Quantity", 25 }

};



Console.WriteLine($"{entity.RowKey}: {entity["Product"]} costs ${entity.GetDouble("Price")}.");

//// Add the newly created entity.

tableClient.AddEntity(entity);



Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{"partitionKey"}'");



// Iterate the <see cref="Pageable"> to access all queried entities.

foreach (TableEntity qEntity in queryResultsFilter)

{

    Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");

}



Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");



//Delete the entity given the partition and row key.

//tableClient.DeleteEntity("partitionKey", "rowKey");

