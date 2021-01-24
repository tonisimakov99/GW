using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWLibrary
{
    public static class ContextKey
    {
        public static string PaymentAccount = "PaymentContext.Payment.Account";
        public static string PaymentSerial = "PaymentContext.Payment.Serial";
        public static string PaymentServerTime = "PaymentContext.Payment.ServerTime";
        public static string PaymentValue = "PaymentContext.Payment.Value";
        public static string PointSerial = "PaymentContext.Point.Serial";
        public static string Payment_erc_balance = "PaymentContext.Payment[\"erc_balance\"]";
        public static string Payment_price = "PaymentContext.Payment[\"price\"]";
        public static string Payment_ADDRID = "PaymentContext.Payment[\"ADDRID\"]";
    }
}
