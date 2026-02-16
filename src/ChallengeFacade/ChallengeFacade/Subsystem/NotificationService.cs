using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFacade.Subsystem
{
    public class NotificationService
    {
        public void SendOrderConfirmation(string email, string orderId)
        {
            Console.WriteLine($"[Notificação] Enviando confirmação de pedido {orderId} para {email}");
        }

        public void SendPaymentReceipt(string email, string transactionId)
        {
            Console.WriteLine($"[Notificação] Enviando recibo de pagamento {transactionId}");
        }

        public void SendShippingNotification(string email, string trackingCode)
        {
            Console.WriteLine($"[Notificação] Enviando código de rastreamento {trackingCode}");
        }
    }
}
