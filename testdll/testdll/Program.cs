using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Retail.Pos;

namespace testdll
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Dynamics.Retail.Pos.PriceService.Price p = new Microsoft.Dynamics.Retail.Pos.PriceService.Price();
            p.GetItemPrice()
        }
    }
}
