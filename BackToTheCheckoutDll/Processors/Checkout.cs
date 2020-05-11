using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll.Processors
{
    public class Checkout
    {
        private IItemProcessor pricingRules;

        public Checkout(IItemProcessor pricingRules)
        {
            this.pricingRules = pricingRules ?? throw new ArgumentNullException(nameof(pricingRules));
        }

        public void Scan(string Item)
        {
            pricingRules.Scan(Item);
        }

        public int Total()
        {
            return pricingRules.Total();
        }
    }
}
