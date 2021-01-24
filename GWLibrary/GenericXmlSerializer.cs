using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GW_Library
{
    public class GenericXmlSerializer
    {
        public T Deserialize<T>(byte[] entity, Encoding encoding)
        {
            using (var stream = new StringReader(encoding.GetString(entity)))
            {
                var serilizer = new XmlSerializer(typeof(T));
                return (T)serilizer.Deserialize(stream);
            }
        }
        public T Deserialize<T>(byte[] entity)
        {
            return Deserialize<T>(entity, UTF8Encoding.UTF8);
        }

        public byte[] Serialize<T>(T entity, XmlWriterSettings xmlWriterSettings, XmlSerializerNamespaces ns, Encoding encoding )
        {
            using (var stream = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                {
                    serializer.Serialize(xmlWriter, entity,ns);
                    return encoding.GetBytes(stream.ToString());
                }
            }
        }
        public byte[] Serialize<T>(T entity)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            return Serialize<T>(entity, settings,ns,Encoding.UTF8);
        }
    }
}
