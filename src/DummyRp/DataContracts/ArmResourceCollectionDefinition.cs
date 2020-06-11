using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyRp.DataContracts
{
    [Serializable]
    public class ArmResourceCollectionDefinition
    {
        [JsonProperty("value")]
        public ArmResourceDefinition[] Value { get; set; }
    }
}
