using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName = "point")]
    public class DepositionPoint
    {
        [XmlElement(ElementName = "id")]
        public int Id;
        [XmlElement(ElementName = "type")]
        public string Type;
        [XmlElement(ElementName = "subagent")]
        public bool Subagent;
        [XmlElement(ElementName = "inn")]
        public string INN;
        [XmlElement(ElementName = "fee")]
        public DepositionPointFee Fee;
        [XmlElement(ElementName = "availabilityType")]
        public string AvailabilityType;
        [XmlElement(ElementName = "address")]
        public DepositionPointAddress Address;
        [XmlElement(ElementName = "office")]
        public string Office;
        [XmlElement(ElementName = "location")]
        public string Location;
    }
}