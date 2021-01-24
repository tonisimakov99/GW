using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName = "paymentParams")]
    public class PaymentParams
    {
        [XmlElement(ElementName = "cardBin")]
        public string CardBin;
        [XmlElement(ElementName = "panMask")]
        public string PanMask;
        [XmlElement(ElementName = "cardProduct")]
        public string CardProduct;
    }
}