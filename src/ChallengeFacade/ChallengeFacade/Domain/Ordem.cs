using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFacade.Domain
{
    public class Ordem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string CustomerEmail { get; set; }
        public string CreditCard { get; set; }
        public string Cvv { get; set; }
        public string ShippingAddress { get; set; }
        public string ZipCode { get; set; }
        public string CouponCode { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
