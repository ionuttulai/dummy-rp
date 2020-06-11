
using System.IO;
using System.Threading.Tasks;

namespace DummyRp
{
    public class DesiredConfigurationStore
    {
        private string currentConfig;
        private int vesrsionNumber;

        private static object lockObj = new object();

        public DesiredConfigurationStore()
        {
            this.vesrsionNumber = 0;
            this.currentConfig = File.ReadAllText(@"AppData\defaultConfiguration.json");
        }

        public Task SetConfigurationAsync(string newConfiguraiton)
        {
            lock(lockObj)
            {
                this.currentConfig = newConfiguraiton;
                this.vesrsionNumber++;
            }

            return Task.CompletedTask;
        }

        public Task<(string, int)> GetCurrentConfiguration()
        {
            return Task.FromResult((this.currentConfig, this.vesrsionNumber));
        }

    }
}
