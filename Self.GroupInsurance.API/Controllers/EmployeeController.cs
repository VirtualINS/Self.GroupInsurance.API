using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Self.GroupInsurance.API.Model;
using Newtonsoft.Json;

namespace Self.GroupInsurance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IOptions<AppConfig> config;

        public EmployeeController(IOptions<AppConfig> config)
        {
            this.config = config;

        }

        // GET: api/Employee
        [HttpGet("GetEmployee")]
        public string Get()
        {
            CloudTableManager ctm = new CloudTableManager(this.config.Value.StorageConnectionString);
            var employees = ctm.GetEmployees();
            if (employees != null)
            {
                List<Employee> emps = employees.Result;
                return JsonConvert.SerializeObject(emps);
            }
            return string.Empty;
         }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/Employee
        [HttpPost("AddEmployee")]
        public string Post([FromBody] Employee value)
        {
            CloudTableManager ctm = new CloudTableManager(this.config.Value.StorageConnectionString);
            string ID = ctm.AddEmployee(value);
            return ID;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
