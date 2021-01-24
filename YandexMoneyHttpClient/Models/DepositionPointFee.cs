using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName = "fee")]
    public class DepositionPointFee
    {
        [XmlElement(ElementName = "type")]
        public string Type;
        [XmlElement(ElementName = "value")]
        public decimal Value;
    }
}