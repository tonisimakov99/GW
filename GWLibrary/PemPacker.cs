using Org.BouncyCastle.Utilities.IO.Pem;
using System.IO;


namespace GW_Library
{
    public class PemPacker
    {
        public string Pack(string header,byte[] content)
        {
            var str = new StringWriter();
            var pemWriter = new PemWriter(str);
            var pemObj = new PemObject(header, content);
            pemWriter.WriteObject(pemObj);
            return str.ToString();
        }
        public byte[] Unpack(string obj)
        {
            var str = new StringReader(obj);
            var pemReader = new PemReader(str);
            var pemObj = pemReader.ReadPemObject();
            return pemObj.Content;
        }
    }
}
