using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFacade.Subsystem
{
    public class CouponSystem
    {
        private Dictionary<string, decimal> _coupons = new Dictionary<string, decimal>
        {
            ["PROMO10"] = 0.10m,
            ["SAVE20"] = 0.20m
        };

        public bool ValidateCoupon(string code)
        {
            Console.WriteLine($"[Cupom] Validando cupom {code}");
            return _coupons.ContainsKey(code);
        }

        public decimal GetDiscount(string code)
        {
            return _coupons.ContainsKey(code) ? _coupons[code] : 0;
        }

        public void MarkCouponAsUsed(string code, string customerId)
        {
            Console.WriteLine($"[Cupom] Marcando cupom {code} como usado por {customerId}");
        }
    }
}
