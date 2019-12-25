using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.GroupInsurance.API.Model
{
    public class State : TableEntity
    {
        public State()
        {
            //this.PartitionKey = Country;
            this.RowKey = Guid.NewGuid().ToString();
            //this.Timestamp = DateTimeOffset.Now;
            //this.ETag = Country;
        }

        public string ID { get; set; }

        public string StateName { get; set; }

        public string Country { get; set; }
    }

}
