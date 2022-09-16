using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "V10001";
            //string str = "Saberin Creative";
            if ((str.Contains("BayState") || str.Contains("Friedman") || str.Contains("Saberin")))
                Console.WriteLine("ok");
            else
                Console.WriteLine("not ok"); Console.Read();
        }
    }
}
