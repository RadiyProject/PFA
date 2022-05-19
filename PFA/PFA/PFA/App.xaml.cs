using PFA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using PFA.Database;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Linq;
using Newtonsoft.Json;
using ServiceReference1;

namespace PFA
{
    public partial class App : Application
    {
        static DBCheque cheques;
        static DBGood goods;
        static DBBudget budget;
        static DBCategory categories;
        public static Service1Client server;
        public static DBCheque Cheques
        {
            get
            {
                if (cheques == null)
                    cheques = new DBCheque(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Cheques.db3"));
                return cheques;
            }
        }
        public static DBGood Goods
        {
            get
            {
                if (goods == null)
                    goods = new DBGood(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Goods.db3"));
                return goods;
            }
        }
        public static DBBudget Budget
        {
            get
            {
                if (budget == null)
                    budget = new DBBudget(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Budget.db3"));
                return budget;
            }
        }
        public static DBCategory Categories
        {
            get
            {
                if (categories == null)
                    categories = new DBCategory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Categories.db3"));
                return categories;
            }
        }
        public App()
        {
            InitializeComponent();
            NavigationPage page;
            server = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
            string user = "";
            if (!Properties.ContainsKey("user"))
                Properties.Add("user", "");
            else
                user = Properties["user"] as string;
            if (user == null || string.IsNullOrEmpty(user))
            {
                page = new NavigationPage(new StartMenu());
                MainPage = page;
            }
            else
            {
                page = new NavigationPage(new MainMenu());
                MainPage = page;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
