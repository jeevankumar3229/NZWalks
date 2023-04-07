using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A
{
    public class C
    {
        private readonly Interface1 i;

        
        
        public int add()
        {
            var a = i.sum();
            return a + 10;
        }
    }
}
