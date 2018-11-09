using System;
using System.Collections.Generic;
using System.Linq;
using DigitalTransformOffice.Service.Helpers;
using Microsoft.Azure.Documents.Client;
public class DBConnection : IDBConnection
{
        public string DbUrl { get; set; } = Settings.Get("dbUrl");
        public string DbKey { get; set; } = Settings.Get("dbKey");
        public string DbName { get; set; } = Settings.Get("dbName");
        public string Collection { get; set; }
        public DBConnection()
        {
            Collection = Settings.Get("collection");
        }
    //  public static string GetSetting(string key) {
    //         return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
    //     }

        public Uri GetCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(DbName, Collection);
        }

         public DocumentClient GetDocumentClient()
        {
            return new DocumentClient(new Uri(DbUrl), DbKey);
        }

        //  DocumentClient dbClient = DBConnection.GetDocumentClient();
        //     Uri collectionUri = DBConnection.GetCollectionUri();

        public IEnumerable<T> GetEmployeeDetails<T>(Func<T, bool> predicate)where T : EmployeeDetails
        {
            return GetDocumentClient().CreateDocumentQuery<T>(GetCollectionUri()).Where(predicate);
        }
}