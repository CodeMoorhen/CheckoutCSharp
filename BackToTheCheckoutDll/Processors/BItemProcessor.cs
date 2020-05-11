using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public class BItemProcessor : AbstractItemProcessor
    {
        public BItemProcessor(IItemProcessor next) : base("B", next)
        { }

        public override int Total()
        {
            int noOfTwo = NoOfOccurences / 2;
            int remainder = NoOfOccurences % 2;
            return Next.Total() + (noOfTwo * 45) + (remainder * 30);
        }
    }    
}
