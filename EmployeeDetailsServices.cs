
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Function
{
    public static class EmployeeDetailsServices
    {
       public static IDBConnection dbcon { get; set; } = new DBConnection();

        [FunctionName("EmployeeDetailsServices")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",
         Route = "getEmployeeDetails/{employeeid}")]HttpRequest req, TraceWriter log, int employeeid)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info("db Key: "+ Environment.GetEnvironmentVariable("dbKey", EnvironmentVariableTarget.Process)
            +"  ||     db Url: " +Environment.GetEnvironmentVariable("dbUrl", EnvironmentVariableTarget.Process)
            +"  ||    db Name: " +Environment.GetEnvironmentVariable("dbName", EnvironmentVariableTarget.Process)
            +"  ||    db Collection: " +Environment.GetEnvironmentVariable("collection", EnvironmentVariableTarget.Process)
            +"  ||    db Connection: " +Environment.GetEnvironmentVariable("dbConnection", EnvironmentVariableTarget.Process)
            );
            EmployeeDetails employeedetails = dbcon.GetEmployeeDetails<EmployeeDetails>(p =>p.employeeid == employeeid).FirstOrDefault();
            
            log.Info($"Records fetch from Database");

            if(employeedetails == null) {
                return new NotFoundObjectResult("Employee not found");
            }
            return (ActionResult)new OkObjectResult(employeedetails);
        }
    }
}
