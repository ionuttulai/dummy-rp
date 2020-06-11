using System;
using System.IO;
using System.Threading.Tasks;
using DummyRp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DummyRp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplicationController : ControllerBase
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        private readonly ILogger _logger;

        public ReplicationController(ILogger<ReplicationController> logger)
        {
            this._logger = logger;
        }

        // POST: api/Replication
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            
            try
            {
                ReplicationPackage data;

                using (var sr = new StreamReader(Request.Body))
                {
                    var strContent = await sr.ReadToEndAsync().ConfigureAwait(false);
                    data = JsonConvert.DeserializeObject<ReplicationPackage>(strContent, ReplicationController.SerializerSettings);
                }

                if (data == null)
                {
                    return BadRequest("Invalid data.");
                }

                foreach (var scopeData in data.Scopes)
                {
                    _logger.LogInformation($"Processing data for scope '{scopeData.ReplicationScope}' with scope eTag '{scopeData.ETag}'...");
                    foreach (var resource in scopeData.UpsertResources)
                    {
                        _logger.LogInformation($"Processing resource id '{resource.Id}' with eTag '{resource.ETag}'");
                        _logger.LogInformation(resource.Data.ToString(Formatting.Indented));
                    }
                }
                return Ok();
            }

            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
