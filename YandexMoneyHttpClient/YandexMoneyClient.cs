using GW_Library;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using YandexMoneyHttpClient.Models.Requests;
using YandexMoneyHttpClient.Models.Responses;

namespace YandexMoneyHttpClient
{
    public class YandexMoneyClient : HttpClient
    {
        private readonly GenericXmlSerializer xmlSerializer;
        private readonly PKCS7Coder pkcs7coder;
        private readonly PemPacker pemPacker;
        private readonly Uri baseUri;
        private const string contentType = "application/pkcs7-mime";
        public YandexMoneyClient(GenericXmlSerializer xmlSerializer, PKCS7Coder pkcs7coder, PemPacker pemPacker, Uri baseUri, WebRequestHandler handler) : base(handler)
        {
            this.xmlSerializer = xmlSerializer;
            this.pkcs7coder = pkcs7coder;
            this.pemPacker = pemPacker;
            this.baseUri = baseUri;
        }

        public TestDepositionResponse TestDepositionRequest(TestDepositionRequest depositionRequest)
        {
            var testUri = new Uri(@"/webservice/deposition/api/testDeposition", UriKind.Relative);
            var xmlRequest = xmlSerializer.Serialize(depositionRequest);
            var response = Request<TestDepositionRequest>(xmlRequest, testUri);

            return xmlSerializer.Deserialize<TestDepositionResponse>(response);
        }
        public MakeDepositionResponse MakeDepositionRequest(MakeDepositionRequest depositionRequest)
        {
            var makeUri = new Uri(@"/webservice/deposition/api/makeDeposition", UriKind.Relative);
            var xmlRequest = xmlSerializer.Serialize(depositionRequest);
            var str = UTF8Encoding.UTF8.GetString(xmlRequest);
            var response = Request<MakeDepositionRequest>(xmlRequest, makeUri);
            return xmlSerializer.Deserialize<MakeDepositionResponse>(response);
        }

        public BalanceResponse BalanceRequest(BalanceRequest balanceRequest)
        {
            var makeUri = new Uri(@"/webservice/deposition/api/balance", UriKind.Relative);
            var xmlRequest = xmlSerializer.Serialize(balanceRequest);
            var response = Request<BalanceRequest>(xmlRequest, makeUri);
            return xmlSerializer.Deserialize<BalanceResponse>(response);
        }

        public AddDepositionPointsResponse AddDepositionPointsRequest(AddDepositionPointsRequest addDepositionPointsRequest)
        {
            var makeUri = new Uri(@"/webservice/deposition/api/addDepositionPoints", UriKind.Relative);
            var xmlRequest = xmlSerializer.Serialize(addDepositionPointsRequest);
            var response = Request<AddDepositionPointsRequest>(xmlRequest, makeUri);
           
            return xmlSerializer.Deserialize<AddDepositionPointsResponse>(response);
        }

        public CheckDepositionPointsResponse CheckDepositionPointsRequest(CheckDepositionPointsRequest checkDepositionPointsRequest)
        {
            var makeUri = new Uri(@"/webservice/deposition/api/checkDepositionPoints", UriKind.Relative);
            var xmlRequest = xmlSerializer.Serialize(checkDepositionPointsRequest);
            var response = Request<CheckDepositionPointsRequest>(xmlRequest, makeUri);
            return xmlSerializer.Deserialize<CheckDepositionPointsResponse>(response);
        }
        private byte[] Request<T>(byte[] xmlRequest, Uri uri)
        {
            var packedXml = Encode(xmlRequest);
            var sendContent = GetHttpContent(packedXml);
            var task = PostAsync(new Uri(baseUri, uri), sendContent);
            task.Wait();
            var response = GetBytesFromHttpContent(task.Result.Content);
            return Decode(response);
        }

        private ByteArrayContent GetHttpContent(byte[] data)
        {
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Headers.ContentLength = data.Length;
            return content;
        }

        private byte[] GetBytesFromHttpContent(HttpContent content)
        {
            var task = content.ReadAsByteArrayAsync();
            task.Wait();
            return task.Result;
        }
        private byte[] Encode(byte[] data)
        {
            var encodedXml = pkcs7coder.Encode(data);
            return Encoding.UTF8.GetBytes(pemPacker.Pack("PKCS7", encodedXml));
        }

        private byte[] Decode(byte[] data)
        {
            var unpackedXml = pemPacker.Unpack(Encoding.UTF8.GetString(data));
            var decodedXml = pkcs7coder.Decode(unpackedXml);
            return decodedXml;
        }
    }
}
