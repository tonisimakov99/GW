using System;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace GW_Library
{
    public class PKCS7Coder
    {
        private readonly X509Certificate2 x509Certificate;

        public PKCS7Coder(X509Certificate2 x509Certificate)
        {
            this.x509Certificate = x509Certificate;
        }

        public byte[] Decode(byte[] data)
        {
            var scms = new SignedCms();
            scms.Decode(data);
            return scms.ContentInfo.Content;
        }

        public byte[] Encode(byte[] data)
        {
            var contentInfo = new ContentInfo(data);
            var scms = new SignedCms(contentInfo);
            scms.Certificates.Add(x509Certificate);
            scms.ComputeSignature(new CmsSigner(x509Certificate));
            return scms.Encode();
        }
    }
}