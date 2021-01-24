using System;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Requests
{
    public class DepositionRequest
    {
        [XmlAttribute(AttributeName ="dstAccount")]
        public string DstAccount;
        [XmlAttribute(AttributeName = "clientOrderId")]
        public string ClientOrderId;
        [XmlAttribute(AttributeName = "requestDT")]
        public DateTime RequestDT;
        [XmlAttribute(AttributeName = "amount")]
        public string Amount;
        [XmlAttribute(AttributeName = "currency")]
        public string Currency;
        [XmlAttribute(AttributeName = "agentId")]
        public long AgentId;
        [XmlAttribute(AttributeName = "contract")]
        public string Contract;
        [XmlAttribute(AttributeName = "depositionPointId")]
        public string DepositionPointId;
        [XmlAttribute(AttributeName = "senderPhone")]
        public string SenderPhone;
        [XmlAttribute(AttributeName = "senderPhoneHash")]
        public string SenderPhoneHash;
        [XmlElement(ElementName ="paymentParams")]
        public PaymentParams Params;
    }
}
