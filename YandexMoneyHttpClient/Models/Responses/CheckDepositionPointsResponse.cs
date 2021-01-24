using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Responses
{
    [XmlType(TypeName ="checkDepositionPointsResponse")]
    public class CheckDepositionPointsResponse
    {
        [XmlAttribute(AttributeName = "requestId")]
        public string RequestId;
        [XmlAttribute(AttributeName = "status")]
        public string Status;
        [XmlElement(ElementName = "error")]
        public Error Error;
        [XmlArray(ElementName ="items")]
        public DepositionPointItem[] Items;
    }
}
