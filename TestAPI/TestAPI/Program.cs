using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            PurchasejournalpostAPI.PurchasejournalpostAPI ws = new PurchasejournalpostAPI.PurchasejournalpostAPI();


            ws.UseDefaultCredentials = false;
            ws.Credentials = new NetworkCredential("MUSEWERX", "g7QotdWv8dETZH6Qv/8JJ6d4uySjKJrbQwGBOGCrS1o=");
            ws.Url = "https://api.businesscentral.dynamics.com/v2.0/d8cf23a2-17d9-41d8-a10d-9aa603abf54d/Sandbox/WS/CRONUS%20USA%2C%20Inc./Codeunit/PurchasejournalpostAPI";


            string inputstring, outputstring;
            inputstring = "microsoft dynamics nav web services!";
            ws.RunCodeUnit();
            Console.ReadLine();
        }
    }
}
