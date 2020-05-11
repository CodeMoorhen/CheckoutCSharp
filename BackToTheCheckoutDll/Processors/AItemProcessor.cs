using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public class AItemProcessor : AbstractItemProcessor
    {
        public AItemProcessor(IItemProcessor next) : base("A", next)
        {}

        public override int Total()
        {
            int noOfThrees = NoOfOccurences / 3;
            int remainder = NoOfOccurences % 3;
            return Next.Total() + (noOfThrees * 130) + (remainder * 50);
        }
    }
}
