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

        public string AddState(State state)
        {
            ct = ctc.GetTableReference("EmployeeStates");
            state.PartitionKey = state.Country;
            TableOperation to = TableOperation.Insert(state);
            ct.ExecuteAsync(to);
            return state.ID.ToString();
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

        public async Task<List<State>> GetStates()
        {
            ct = ctc.GetTableReference("EmployeeStates");
            TableQuery<State> tq = new TableQuery<State>();
            TableQuerySegment<State> tqs = await ct.ExecuteQuerySegmentedAsync<State>(tq, null);
            List<State> states = tqs.Results;
            return states;
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

        public async Task<State> GetState(string ID)
        {
            ct = ctc.GetTableReference("EmployeeStates");
            TableQuery<State> tq = new TableQuery<State>().Where(
                TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, ID));

            TableQuerySegment<State> tqs = await ct.ExecuteQuerySegmentedAsync<State>(tq, null);
            List<State> states = tqs.Results;
            return states[0];
        }

        public bool DeleteEmployee(string ID)
        {
            return false;
        }

    }
}
