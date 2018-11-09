using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IDBConnection
{
        Uri GetCollectionUri();
        DocumentClient GetDocumentClient();
        IEnumerable<T> GetEmployeeDetails<T>(Func<T, bool> predicate) where T : EmployeeDetails;
 }