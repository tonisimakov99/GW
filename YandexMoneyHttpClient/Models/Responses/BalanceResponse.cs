using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Responses
{
    [XmlType(TypeName = "balanceResponse")]
    public class BalanceResponse
    {
        [XmlAttribute(AttributeName = "status")]
        public int Status;
        [XmlAttribute(AttributeName = "error")]
        public int Error;
        [XmlAttribute(AttributeName = "clientOrderId")]
        public string ClientOrderId;
        [XmlAttribute(AttributeName = "processedDT")]
        public DateTime ProcessedDt;
        [XmlAttribute(AttributeName = "balance")]
        public decimal Balance;
    }
}
