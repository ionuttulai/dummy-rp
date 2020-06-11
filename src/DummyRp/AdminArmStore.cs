using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DummyRp
{
    public class AdminArmStore
    {
        private string metricsData;
        private string alertsData;
        private string availabilitySetsData;

        public AdminArmStore()
        {
            this.metricsData = File.ReadAllText(@"AppData\defaultMetricsData.json");
            this.alertsData = File.ReadAllText(@"AppData\defaultAlertsData.json");
            this.availabilitySetsData = File.ReadAllText(@"AppData\defaultAvailabilitySets.json");
        }

        public Task SetMetricsAsync(string metricsData)
        {
            this.metricsData = metricsData;
            return Task.CompletedTask;
        }

        public Task<string> GetMetricsAsync()
        {
            return Task.FromResult(this.metricsData);
        }

        public Task SetAlertsAsync(string alertsData)
        {
            this.alertsData = alertsData;
            return Task.CompletedTask;
        }

        public Task<string> GetAlertsAsync()
        {
            return Task.FromResult(this.alertsData);
        }

        public Task SetAvailabilitySetsAsync(string availabilitySetsData)
        {
            this.availabilitySetsData = availabilitySetsData;
            return Task.CompletedTask;
        }

        public Task<string> GetAvailabilitySetsAsync()
        {
            return Task.FromResult(this.availabilitySetsData);
        }
    }
}
