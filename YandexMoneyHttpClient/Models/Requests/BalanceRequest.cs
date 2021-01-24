using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models.Requests
{
    [XmlType(TypeName = "balanceRequest")]
    public class BalanceRequest
    {
        [XmlAttribute(AttributeName ="clientOrderId")]
        public string ClientOrderId;
        [XmlAttribute(AttributeName = "requestDT")]
        public DateTime RequestDT;
        [XmlAttribute(AttributeName = "agentId")]
        public long AgentId;
    }
}
