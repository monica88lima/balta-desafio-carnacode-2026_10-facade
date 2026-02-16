using ChallengeFacade.Domain;
using ChallengeFacade.Subsystem;


namespace ChallengeFacade.Facade
{
    public class OrderFacade
    {
        private readonly InventorySystem _inventorySystem;
        private readonly PaymentGateway _paymentSystem;
        private readonly ShippingService _shippingSystem;
        private readonly NotificationService _notificationSystem;
        private readonly CouponSystem _couponSystem;

        public OrderFacade()
        {
            _inventorySystem = new InventorySystem();
            _paymentSystem = new PaymentGateway();
            _shippingSystem = new ShippingService();
            _notificationSystem = new NotificationService();
            _couponSystem = new CouponSystem();

        }
        public void Process(Ordem ordem)
        {
            try
            {
                // Passo 1: Verificar estoque
                if (!_inventorySystem.CheckAvailability(ordem.ProductId))
                {
                    Console.WriteLine("❌ Produto indisponível");
                    return;
                }
                // Passo 2: Reservar produto
                _inventorySystem.ReserveProduct(ordem.ProductId, ordem.Quantity);
                // Passo 3: Validar e aplicar cupom
                decimal discount = 0;
                if (!string.IsNullOrEmpty(ordem.CouponCode))
                {
                    if (_couponSystem.ValidateCoupon(ordem.CouponCode))
                    {
                        discount = _couponSystem.GetDiscount(ordem.CouponCode);
                    }
                }
                // Passo 4: Calcular valores
                decimal subtotal = ordem.ProductPrice * ordem.Quantity;
                decimal discountAmount = subtotal * discount;
                decimal shippingCost = _shippingSystem.CalculateShipping(ordem.ZipCode, ordem.Quantity * 0.5m);
                decimal total = subtotal - discountAmount + shippingCost;

                // Passo 5: Processar pagamento
                string transactionId = _paymentSystem.InitializeTransaction(total);

                if (!_paymentSystem.ValidateCard(ordem.CreditCard, ordem.Cvv))
                {
                    _inventorySystem.ReleaseReservation(ordem.ProductId, ordem.Quantity);
                    Console.WriteLine("❌ Cartão inválido");
                    return;
                }

                if (!_paymentSystem.ProcessPayment(transactionId, ordem.CreditCard))
                {
                    _inventorySystem.ReleaseReservation(ordem.ProductId, ordem.Quantity);
                    Console.WriteLine("❌ Pagamento recusado");
                    return;
                }

                // Passo 6: Criar envio
                string orderId = $"ORD{DateTime.Now.Ticks}";
                string labelId = _shippingSystem.CreateShippingLabel(orderId, ordem.ShippingAddress);
                _shippingSystem.SchedulePickup(labelId, DateTime.Now.AddDays(1));

                // Passo 7: Marcar cupom como usado
                if (!string.IsNullOrEmpty(ordem.CouponCode))
                {
                    _couponSystem.MarkCouponAsUsed(ordem.CouponCode, ordem.CustomerEmail);
                }

                // Passo 8: Enviar notificações
                _notificationSystem.SendOrderConfirmation(ordem.CustomerEmail, orderId);
                _notificationSystem.SendPaymentReceipt(ordem.CustomerEmail, transactionId);
                _notificationSystem.SendShippingNotification(ordem.CustomerEmail, labelId);

                Console.WriteLine($"\n✅ Pedido {orderId} finalizado com sucesso!");
                Console.WriteLine($"   Total: R$ {total:N2}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"❌ Erro ao processar pedido: {ex.Message}");
            }

        }


    }

}
