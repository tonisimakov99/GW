using System;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Responses
{
    public class DepositionResponse
    {
        [XmlAttribute(AttributeName = "status")]
        public int Status;
        [XmlAttribute(AttributeName = "error")]
        public int Error;
        [XmlAttribute(AttributeName = "clientOrderId")]
        public string ClientOrderId;
        [XmlAttribute(AttributeName = "processedDT")]
        public DateTime ProcessedDT;
        [XmlAttribute(AttributeName = "balance")]
        public decimal Balance;
        [XmlAttribute(AttributeName = "techMessage")]
        public string TechMessage;
        [XmlAttribute(AttributeName = "identification")]
        public string Identification;
    }
}
