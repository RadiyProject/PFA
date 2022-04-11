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
        static DBCheque cheques;
        static DBGood goods;
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
        public App()
        {
            InitializeComponent();
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
