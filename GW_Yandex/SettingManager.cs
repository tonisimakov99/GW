using IBP.SDKGatewayLibrary;
namespace GW_Library
{
    public class SettingManager : SettingManagerBase
    {
        public override string[] GetFilterContextKeys()
        {
            return new[] { "1", "2" };
        }

        public override string[] GetPaymentContextKeys(Operation operation)
        {
            return new[] {
                "PaymentContext.Payment.Account",
                "PaymentContext.Payment.Id",
                "PaymentContext.Payment.ServerTime",
                "PaymentContext.Payment.Value",
                "PaymentContext.Point.Serial"
            };
        }

        public override string[] GetSettingsKey()
        {
            return new[] { 
                SettingsFields.YandexURI, 
                SettingsFields.CertificatePathPFX, 
                SettingsFields.Currency, 
                SettingsFields.CertificatePassword,
                SettingsFields.AgentId };
        }

        public override void InitSettingManadger(string initString)
        {
            
        }

        public override string[] SaveSettingKey()
        {
            return null;
        }

        public override string[] SetPaymentContextKeys(Operation operation)
        {
            return null;
        }
    }
}
