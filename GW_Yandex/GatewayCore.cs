
using GW_Yandex;
using IBP.SDKGatewayLibrary;
using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using YandexMoneyHttpClient;
using YandexMoneyHttpClient.Models.Requests;

namespace GW_Library
{
    public class GatewayCore : GatewayCoreBase
    {
        private YandexMoneyClient client;
        private long agentId;
        private string currency;
        private Logger logger;
        public override void CheckAccount(ref Context context)
        {
            var request = new TestDepositionRequest()
            {
                AgentId = agentId,
                ClientOrderId = ((Guid)context["PaymentContext.Payment.Id"]).ToString(),
                RequestDT = (DateTime)context["PaymentContext.Payment.ServerTime"],
                DstAccount = (string)context["PaymentContext.Payment.Account"],
                Amount = (1).ToString(".00", CultureInfo.InvariantCulture),
                Currency = currency,
                Contract = "Зачисление на кошелек",
                DepositionPointId = (context["PaymentContext.Point.Serial"]).ToString()
            };

            var response = client.TestDepositionRequest(request);

            if (response.Status == 0)
                context.Status = State.AccountExists;
            if (response.Status == 1)
                context.Status = State.DenialOfService;
            if (response.Status == 3)
                context.Status = State.AccountNotExists;

            logger.WriteMessage($"request {request.DstAccount} {request.Amount}",1);

            logger.WriteMessage($"check status {response.Status}: {OperationStatuses.Instance[response.Status]}", 1);
            logger.WriteMessage($"check error {response.Error}: {ErrorCodes.Instance[response.Error]}", 3);
        }

        public override void CheckProcessStatus(ref Context context)
        {
            
        }

        public override void CheckRecallStatus(ref Context context)
        {
            
        }

        public override void Dispose()
        {
            client.Dispose();
        }
        
        public override void InitGateway(Hashtable settigs)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var uri = new Uri(settigs[SettingsFields.YandexURI].ToString());
            var serializer = new GenericXmlSerializer();
            var certificate = new X509Certificate2((string)settigs[SettingsFields.CertificatePathPFX], (string)settigs[SettingsFields.CertificatePassword]);
            var coder = new PKCS7Coder(certificate);
            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(certificate);
            var pemPacker = new PemPacker();
            client = new YandexMoneyClient(serializer, coder, pemPacker, uri, handler);
            agentId = long.Parse(settigs[SettingsFields.AgentId].ToString());
            currency = settigs[SettingsFields.Currency].ToString();
            logger = Logger.Instance;

            logger.WriteMessage("Init success", 1);
        }
        public override void Process(ref Context context)
        {
            var request = new MakeDepositionRequest()
            {
                AgentId = agentId,
                ClientOrderId = ((Guid)context["PaymentContext.Payment.Id"]).ToString(),
                RequestDT = (DateTime)context["PaymentContext.Payment.ServerTime"],
                DstAccount = (string)context["PaymentContext.Payment.Account"],
                Amount = ((decimal)context["PaymentContext.Payment.Value"]).ToString(".00", CultureInfo.InvariantCulture),
                Currency = currency,
                Contract = "Зачисление на кошелек",
                DepositionPointId = (context["PaymentContext.Point.Serial"]).ToString()
            };

            var response = client.MakeDepositionRequest(request);

            if (response.Status == 0)
                context.Status = State.Finalized;
            if (response.Status == 1)
                context.Status = State.Processing;
            if (response.Status == 3)
                context.Status = State.Rejected;

            logger.WriteMessage($"request {request.DstAccount} {request.Amount}",1);

            logger.WriteMessage($"process status {response.Status}: {OperationStatuses.Instance[response.Status]}", 1);
            logger.WriteMessage($"process error {response.Error}: {ErrorCodes.Instance[response.Error]}", 3);
        }

        public override void RecallPayment(ref Context context)
        {
            
        }

        public override Hashtable SaveSettings()
        {
            return null;
        }
    }
}
