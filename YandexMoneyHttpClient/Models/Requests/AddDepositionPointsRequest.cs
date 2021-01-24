using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Requests
{
    [XmlType(TypeName = "addDepositionPointsRequest")]
    public class AddDepositionPointsRequest
    {
        [XmlAttribute(AttributeName = "requestId")]
        public string RequestId;
        [XmlAttribute(AttributeName = "agentId")]
        public long AgentId;
        [XmlArray(ElementName = "points")]
        public DepositionPoint[] Points;
    }

}
