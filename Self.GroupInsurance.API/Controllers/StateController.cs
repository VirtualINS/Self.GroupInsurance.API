using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Self.GroupInsurance.API.Model;

namespace Self.GroupInsurance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IOptions<AppConfig> config;

        public StateController(IOptions<AppConfig> config)
        {
            this.config = config;
        }

        // GET: api/State
        [HttpGet("GetStates")]
        public string Get()
        {
            CloudTableManager ctm = new CloudTableManager(this.config.Value.StorageConnectionString);
            var states = ctm.GetStates();
            if (states != null)
            {
                List<State> sts = states.Result;
                return JsonConvert.SerializeObject(sts);
            }
            return string.Empty;
        }

        // GET: api/State/5
        [HttpGet("{id}", Name = "GetState")]
        public string GetState(int id)
        {
            return "value";
        }

        // POST: api/State
        [HttpPost("AddState")]
        public string Post([FromBody] State value)
        {
            CloudTableManager ctm = new CloudTableManager(this.config.Value.StorageConnectionString);
            string ID = ctm.AddState(value);
            return ID;
        }

        // PUT: api/State/5
        [HttpPut("{id}")]
        public void UpdateState(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteState(int id)
        {
        }
    }
}
