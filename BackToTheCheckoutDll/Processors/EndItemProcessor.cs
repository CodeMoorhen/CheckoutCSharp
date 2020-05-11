using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutDll
{
    public class EndItemProcessor : IItemProcessor
    {
        public void Scan(string item)
        {
            throw new ApplicationException("Something has gone wrong");
        }

        public int Total()
        {
            return 0;
        }
    }
}
