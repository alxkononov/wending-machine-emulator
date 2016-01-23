using System;
using Microsoft.Owin.Hosting;
using System.Configuration;

namespace wending_machine_emulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = ConfigurationManager.AppSettings["port"];
            var baseAddress = string.Format("http://localhost:{0}/", port);
            

            // Start OWIN host 
            WebApp.Start<Startup>(baseAddress);
           

            Console.ReadLine();
        }
    }
}
