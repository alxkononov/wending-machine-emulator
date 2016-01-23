using System;
using Microsoft.Owin.Hosting;
using System.Configuration;
using wending_machine_emulator.Models;

namespace wending_machine_emulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = ConfigurationManager.AppSettings["port"];
            var baseAddress = string.Format("http://localhost:{0}/", port);

            var wallet = new Wallet();

            wallet[Nominals.Ten] = 10;
            wallet[Nominals.Two] = 2;
            wallet[Nominals.One] = 100;

            var wallet2 = new Wallet();

            wallet2.Flush(wallet, false);

            // Start OWIN host 
            WebApp.Start<Startup>(baseAddress);
           

            Console.ReadLine();
        }
    }
}
