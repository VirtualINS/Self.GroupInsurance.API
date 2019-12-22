using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.GroupInsurance.API.Model
{
    public class Dependent
    {
        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Relation { get; set; }

        public Employee ParentEmployee { get; set; }
    }
}
