using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Requests
{
    [XmlType(TypeName = "checkDepositionPointsRequest")]
    public class CheckDepositionPointsRequest
    {
        [XmlAttribute(AttributeName = "agentId")]
        public long AgentId;
        [XmlAttribute(AttributeName = "requestId")]
        public string RequestId;
    }
}
