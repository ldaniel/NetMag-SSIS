using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetMag.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DTSX client = new DTSX();
            string result;

            result = client.ExecutarPacoteLocal(@"C:\Documents and Settings\Temporario\Desktop\Artigos\Solution\NetMag.Solution\NetMag.ETL\bin\LoadReviewer.dtsx");

            System.Console.Write(result);
            System.Console.ReadKey();

            result = client.ExecutarPacoteRemoto(@"Data Source=(local);Initial Catalog=msdb;Integrated Security=SSPI", "LoadReviewerJob");
            System.Console.Write(result);
            System.Console.ReadKey();            
        }
    }
}
