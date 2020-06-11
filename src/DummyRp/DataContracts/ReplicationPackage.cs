using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyRp.DataContracts
{
    [Serializable]
    public class ReplicationPackage
    {
        public ReplicationScopeData[] Scopes { get; set; }
    }

    [Serializable]
    public class ReplicationScopeData
    {
        public string ReplicationScope { get; set; }

        public string ETag { get; set; }

        public string DeltaStartETag { get; set; }

        public long TotalResourceCount { get; set; }

        public TimeSpan? CollectionTimeCost { get; set; }

        public JToken Errors { get; set; }

        public ResourceData[] UpsertResources { get; set; }

        public string[] DeleteResources { get; set; }
    }

    [Serializable]
    public class ResourceData
    {
        public string Id { get; set; }

        public string ApiVersion { get; set; }

        public JToken Data { get; set; }

        public string ETag { get; set; }
    }
}
