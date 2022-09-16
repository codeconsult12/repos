using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soap
{
    class Program
    {
        static void Main(string[] args)
        {
            PJ_WebRef.PurchaseJournalPost ws = new PJ_WebRef.PurchaseJournalPost();
            ws.UseDefaultCredentials = false;
            ws.Credentials = new NetworkCredential("EHTISHAM", "aWAn6JAPNmdkv/arv3bVbNamwuAJnv3yuyakWQSduD8=");
            ws.Url = "https://api.businesscentral.dynamics.com/v2.0/d8cf23a2-17d9-41d8-a10d-9aa603abf54d/Production/WS/CRONUS%20USA%2C%20Inc./Codeunit/PurchaseJournalPost";

            string root = @"C:\ErrorLogBusinessCentral";
            // If directory does not exist, don't even try   
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string path = @"C:\ErrorLogBusinessCentral\Error.txt";
            if (!File.Exists(path))
            {
                string fileName = @"C:\ErrorLogBusinessCentral\Error.txt";
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Error Log:");
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString());
                    fs.Write(author, 0, author.Length);
                }
            }

            try
            {
                Console.WriteLine("What is your batch name?");
                string batchname = Console.ReadLine();
                if (batchname != "")
                {
                    string SuccessMsg = ws.RunCodeUnit(ref batchname);
                    if (SuccessMsg == "Success")
                    {
                        Console.WriteLine("Successfully posted purchase journal entries!");
                        Console.WriteLine("Deleting current batch....");
                        try
                        {
                            ws.DeleteCurrentBatch(ref batchname);
                            Console.WriteLine("Successfully deleted batch!");
                        }
                        catch (Exception ex1)
                        {
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("Error " + DateTime.Now.ToString() + " : journal successfully post but batch couldn't delete. \n" + ex1.Message);
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine("Un - Successful " + DateTime.Now.ToString() + " : Due to some reason purchase journal cannot post!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    Console.WriteLine("Error: " + ex.Message);
                    sw.WriteLine("Error: " + DateTime.Now + " " + ex.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
