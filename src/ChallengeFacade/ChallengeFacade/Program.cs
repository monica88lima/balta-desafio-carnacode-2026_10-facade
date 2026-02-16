// See https://aka.ms/new-console-template for more information
using ChallengeFacade.Domain;
using ChallengeFacade.Facade;


Console.WriteLine("=== Sistema de E-commerce ===\n");
var order = new Ordem
{
    ProductId = "PROD001",
    Quantity = 2,
    CustomerEmail = "cliente@email.com",
    CreditCard = "1234567890123456",
    Cvv = "123",
    ShippingAddress = "Rua Exemplo, 123",
    ZipCode = "12345-678",
    CouponCode = "PROMO10",
    ProductPrice = 100.00m
};

Console.WriteLine("=== Processando Pedido (Código Complexo) ===\n");
var orderFacade = new OrderFacade();
orderFacade.Process(order);