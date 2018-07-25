using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using System.Collections.Generic;
using TableClients;

namespace Kitchenary
{
    public class TableActions
    {
        public static void AddRow (string tableName, TableEntity myTableEntity)
        {
            string storageAccountName = ConfigurationManager.AppSettings["StorageAccount"];
            string storageAccountKey = ConfigurationManager.AppSettings["StorageKey"];
            CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName,storageAccountKey), true);
            var tableClient = storageAccount.CreateCloudTableClient();
            CloudTable myTable = tableClient.GetTableReference(tableName);
            myTable.CreateIfNotExists();
            TableOperation insertOperation = TableOperation.InsertOrReplace(myTableEntity);
            myTable.Execute(insertOperation);
        }

        public static IEnumerable<TableEntity> GetResult (string tableName,string user)
        {
            string storageAccountName = ConfigurationManager.AppSettings["StorageAccount"];
            string storageAccountKey = ConfigurationManager.AppSettings["StorageKey"];
            CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var tableClient = storageAccount.CreateCloudTableClient();
            CloudTable myTable = tableClient.GetTableReference(tableName);
            //build filter string
            string myTableFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user);
            TableQuery<TableEntity> tableQuery = new TableQuery<TableEntity>().Where(myTableFilter);
            return myTable.ExecuteQuery(tableQuery);

        }
        public static IEnumerable<PantryEntity> GetPantryResult(string tableName, string user)
        {
            string storageAccountName = ConfigurationManager.AppSettings["StorageAccount"];
            string storageAccountKey = ConfigurationManager.AppSettings["StorageKey"];
            CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var tableClient = storageAccount.CreateCloudTableClient();
            CloudTable myTable = tableClient.GetTableReference(tableName);
            //build filter string
            string myTableFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user);
            TableQuery<PantryEntity> tableQuery = new TableQuery<PantryEntity>().Where(myTableFilter);
            return myTable.ExecuteQuery(tableQuery);

        }


    }
}
