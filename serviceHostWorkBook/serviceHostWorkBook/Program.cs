using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCFLibraryForTP;
using System.ServiceModel.Description;

namespace serviceHostWorkBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHost host = new ServiceHost(typeof(WCF_Library.WorkBook), new Uri("http://localhost:8733/Design_Time_Addresses/WCF_Library/Service1/"));
            //host.AddServiceEndpoint(typeof(WCF_Library.IWorkBook), new BasicHttpBinding(), new Uri("http://localhost:8733/Design_Time_Addresses/WCF_Library/Service1/"));

            ServiceHost host = new ServiceHost(typeof(WCFLibraryForTP.Service1));

            //ServiceMetadataBehavior beahvior = new ServiceMetadataBehavior();
            //beahvior.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(beahvior);
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), new Uri("http://localhost:8080/bookservice/mex"));

            host.Open();
            Console.WriteLine("Служба запущена");
            Console.ReadLine();
            host.Close();
        }
    }
    }
