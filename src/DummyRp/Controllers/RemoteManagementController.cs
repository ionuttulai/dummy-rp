
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
    public class RemoteManagementController : ControllerBase
    {
        private readonly DesiredConfigurationStore configStore;

        public RemoteManagementController(DesiredConfigurationStore configStore)
        {
            this.configStore = configStore;
        }

        // GET /remoteManagement/desiredState
        [HttpGet("desiredState")]
        public async Task<ActionResult> GetDesiredState()
        {
            (string, int) data = await this.configStore.GetCurrentConfiguration().ConfigureAwait(false);

           
            var response = new DesiredStateResponse()
            {
                Configuration = data.Item1 != null ? JObject.Parse(data.Item1) : null,
                ConfigVersion = "version_" + data.Item2
            };

            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        // PUT /remoteManagement/desiredState
        [HttpPut("desiredState")]
        public async Task<ActionResult> PutDesiredState()
        {
            using (var sr = new StreamReader(Request.Body))
            {
                string json = await sr.ReadToEndAsync().ConfigureAwait(false);
                JToken j = JToken.Parse(json);

                await this.configStore.SetConfigurationAsync(j.ToString()).ConfigureAwait(false);
            }
                        
            
            return Ok();
        }


    }
}