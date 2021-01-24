using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName = "error")]
    public class Error
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code;
        [XmlAttribute(AttributeName = "pointId")]
        public int PointId;
    }
}
