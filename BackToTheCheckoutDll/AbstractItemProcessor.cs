using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public abstract class AbstractItemProcessor : IItemProcessor
    {
        protected IItemProcessor Next;
        protected int NoOfOccurences;
        private string ItemType;

        public AbstractItemProcessor(string ItemType, IItemProcessor next)
        {
            this.Next = next;
            this.ItemType = ItemType;
        }

        public void Scan(string item)
        {
            if (ItemType == item)
            {
                NoOfOccurences++;
            }
            else
            {
                Next.Scan(item);
            }
        }

        public abstract int Total();
    }
}
