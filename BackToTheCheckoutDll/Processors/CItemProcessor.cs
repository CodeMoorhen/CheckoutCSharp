using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public class CItemProcessor : AbstractItemProcessor
    {
        public CItemProcessor(IItemProcessor next) : base("C", next)
        { }

        public override int Total()
        {
            return Next.Total() + (NoOfOccurences * 20);
        }
    }
}
