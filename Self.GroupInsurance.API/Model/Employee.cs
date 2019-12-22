using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.GroupInsurance.API.Model
{
    public class Employee : TableEntity
    {
        public Employee()
        {
            this.PartitionKey = LastName;
            this.RowKey = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
