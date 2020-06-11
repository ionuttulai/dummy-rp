using System;
using System.Threading.Tasks;
using DummyRp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DummyRp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DummyArmController : ControllerBase
    {
        private readonly AdminArmStore armStore;
        public DummyArmController(AdminArmStore armStore)
        {
            this.armStore = armStore;
        }

        [HttpGet("subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{providerNamespace}/{resourceType}")]
        public async Task<ActionResult> GetGroupWideResourceType(string subscriptionId, string resourceGroup, string providerNamespace, string resourceType)
        {
            string data;

            // "/subscriptions/$defaultSubscription/resourceGroups/system.$location/providers/Microsoft.Compute/availabilitySets"
            if (string.Equals(resourceType, "availabilitySets", StringComparison.InvariantCultureIgnoreCase))
            {
                data = await this.armStore.GetAvailabilitySetsAsync().ConfigureAwait(false);
            }
            // no resource found
            else return new ContentResult()
            {
                StatusCode = 404
            };

            var response = JsonConvert.DeserializeObject<ArmResourceCollectionDefinition>(data);

            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        [HttpGet("subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{providerNamespace}/{resourceType}/{resource}")]
        public async Task<ActionResult> GetGroupWideResource(string subscriptionId, string resourceGroup, string providerNamespace, string resourceType, string resource)
        {
            string data;

            // "/subscriptions/$defaultSubscription/resourceGroups/system.$location/providers/Microsoft.InfrastructureInsights.Admin/regionHealths/$location"
            if (string.Equals(resource, "local", StringComparison.InvariantCultureIgnoreCase) && string.Equals(resourceType, "regionHealths", StringComparison.InvariantCultureIgnoreCase))
            {
                data = await this.armStore.GetMetricsAsync().ConfigureAwait(false);
            }
            // no resource found
            else return new ContentResult()
            {
                StatusCode = 404
            };

            var response = JsonConvert.DeserializeObject<ArmResourceDefinition>(data);

            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        [HttpGet("subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{providerNamespace}/{resourceType}/{resource}/{proxyResourceType}")]
        public async Task<ActionResult> GetGroupWideProxyResource(string subscriptionId, string resourceGroup, string providerNamespace, string resourceType, string resource, string proxyResourceType)
        {
            string data;
            // "/subscriptions/$defaultSubscription/resourceGroups/system.$location/providers/Microsoft.InfrastructureInsights.Admin/regionHealths/$location/alerts"
            if (string.Equals(proxyResourceType, "alerts", StringComparison.InvariantCultureIgnoreCase))
            {
                data = await this.armStore.GetAlertsAsync().ConfigureAwait(false);
            }
            // no resource found
            else return new ContentResult()
            {
                StatusCode = 404
            };

            var response = JsonConvert.DeserializeObject<ArmResourceCollectionDefinition>(data);
            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = 200,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        
        
    }
}