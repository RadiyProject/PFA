using PFA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using PFA.Database;
namespace PFA
{
    public partial class App : Application
    {
        static Database_receipt databaseReceit;
        static Database_product databaseProduct;
        public static Database_receipt Database_receipt
        {
            get
            {
                if (databaseReceit == null)
                {
                    databaseReceit = new Database_receipt(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Receipt.db3"));
                }
                return databaseReceit;
            }
        }
        public static Database_product Database_Product
        {
            get
            {
                if (databaseProduct == null)
                {
                    databaseProduct = new Database_product(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Product.db3"));
                }
                return databaseProduct;
            }
        }
        public App()
        {
            InitializeComponent();
            //NavigationPage page = new NavigationPage(new MainMenu());
            NavigationPage page = new NavigationPage(new StartMenu());
            MainPage = page;
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
