using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseApp
{
    public class Table
    {
        public static void Main()
        {
            // Construct a new "TableServiceClient using a TableSharedKeyCredential.

            var serviceClient = new TableServiceClient(
            new Uri("\t\r\nhttps://sa22sep.table.core.windows.net/product"),
            new TableSharedKeyCredential("sa22sep", "JpOi7lplh0rBPKegBf4FoZnvzioGrojstsONqjJvF51tgIoPzJuEXe1Cb/el0bD7NJaj3a7nCq3v+AStG/E0/g=="));

            // Create a new table. The TableItem class stores properties of the created table.
            string tableName = "OfficeSupplies1p1";
            TableItem table = serviceClient.CreateTableIfNotExists(tableName);
            Console.WriteLine($"The created table's name is {table.Name}.");

            // Use the <see cref="TableServiceClient"> to query the service. Passing in OData filter strings is optional.

            Pageable<TableItem> queryTableResults = serviceClient.Query(filter: $"TableName eq '{tableName}'");

            Console.WriteLine("The following are the names of the tables in the query results:");

            // Iterate the <see cref="Pageable"> in order to access queried tables.

            foreach (TableItem t in queryTableResults)
            {
                Console.WriteLine(t.Name);
            }
        }
    }
}
