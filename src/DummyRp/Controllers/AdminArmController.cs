
using System.IO;
using System.Threading.Tasks;
using DummyRp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DummyRp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminArmController : ControllerBase
    {
        private readonly AdminArmStore armStore;
        public AdminArmController(AdminArmStore armStore)
        {
            this.armStore = armStore;
        }

        [HttpGet("metrics")]
        public async Task<ActionResult> GetMetrics()
        {
            var data = await this.armStore.GetMetricsAsync().ConfigureAwait(false); 
            var response = JsonConvert.DeserializeObject<ArmResourceDefinition>(data);

            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        [HttpPut("metrics")]
        public async Task<ActionResult> PutMetrics()
        {
            using (var sr = new StreamReader(Request.Body))
            {
                string json = await sr.ReadToEndAsync().ConfigureAwait(false);
                JToken j = JToken.Parse(json);

                await this.armStore.SetMetricsAsync(j.ToString()).ConfigureAwait(false); 
            }

            return Ok();
        }

        [HttpGet("alerts")]
        public async Task<ActionResult> GetAlerts()
        {
            var data = await this.armStore.GetAlertsAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<ArmResourceCollectionDefinition>(data);

            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        [HttpPut("alerts")]
        public async Task<ActionResult> PutAlerts()
        {
            using (var sr = new StreamReader(Request.Body))
            {
                string json = await sr.ReadToEndAsync().ConfigureAwait(false);
                JToken j = JToken.Parse(json);

                await this.armStore.SetAlertsAsync(j.ToString()).ConfigureAwait(false); 
            }


            return Ok();
        }


    }
}