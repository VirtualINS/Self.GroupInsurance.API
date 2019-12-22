using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using Self.GroupInsurance.API.Model;

namespace Self.GroupInsurance.API
{
    public class CloudTableManager
    {
        CloudStorageAccount csa;
        CloudTableClient ctc;
        CloudTable ct;
        

        public string connectionString;

        public CloudTableManager(string connectionString)
        {
            this.connectionString = connectionString;
            csa = CloudStorageAccount.Parse(this.connectionString);
            ctc = csa.CreateCloudTableClient();
            
        }

        public string AddEmployee(Employee employee)
        {
            ct = ctc.GetTableReference("Employees");
            employee.PartitionKey = employee.LastName;
            TableOperation to = TableOperation.Insert(employee);
            ct.ExecuteAsync(to);
            return employee.ID.ToString();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            ct = ctc.GetTableReference("Employees");
            //TableQuery<Employee> tq = new TableQuery<Employee>().Where(
            //    TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, value));

            TableQuery<Employee> tq = new TableQuery<Employee>();
            TableQuerySegment<Employee> tqs = await ct.ExecuteQuerySegmentedAsync<Employee>(tq, null);
            List<Employee> emps = tqs.Results;
            return emps;
        }

        public async Task<Employee> GetEmployees(string ID)
        {
            ct = ctc.GetTableReference("Employee");
            TableQuery<Employee> tq = new TableQuery<Employee>().Where(
                TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, ID));

            TableQuerySegment<Employee> tqs = await ct.ExecuteQuerySegmentedAsync<Employee>(tq, null);
            List<Employee> emps = tqs.Results;
            return emps[0];
        }

        public bool DeleteEmployee(string ID)
        {
            return false;
        }

    }
}
