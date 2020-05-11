using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public class DItemProcessor : AbstractItemProcessor
    {
        public DItemProcessor(IItemProcessor next) : base("D", next)
        { }

        public override int Total()
        {
            return Next.Total() + (NoOfOccurences * 15);
        }
    }
}
