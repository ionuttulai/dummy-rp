using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyRp.DataContracts
{
    [Serializable]
    public class DesiredStateResponse
    {
        public string ConfigVersion { get; set; }

        public JToken Configuration { get; set; }

        internal DesiredStateRequestError ToDesiredStateRequestError(DesiredStateRequestErrorType errorType, string errorMessage)
        {
            return new DesiredStateRequestError()
            {
                Request = this,
                ErrorType = errorType,
                ErrorString = errorMessage
            };
        }
    }

    public class DesiredStateRequestError
    {
        public DesiredStateRequestErrorType ErrorType { get; set; }

        public string ErrorString { get; set; }

        public DesiredStateResponse Request { get; set; }
    }

    public enum DesiredStateRequestErrorType
    {
        ValidationFailed,

        ParsingFailed
    }
}
