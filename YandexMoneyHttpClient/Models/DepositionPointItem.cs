using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName ="item")]
    public class DepositionPointItem
    {
        [XmlAttribute(AttributeName ="id")]
        public int Id;
        [XmlAttribute(AttributeName ="status")]
        public string Status;
        [XmlAttribute(AttributeName ="reason")]
        public string Reason;
    }
}