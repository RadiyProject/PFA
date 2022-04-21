using PFA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using PFA.Database;
using System.Threading.Tasks;

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
            /*if (Cheques != null)
                foreach (Cheque cheq in Task.Run(() => Cheques.GetAsync()).Result) //Если нужно очистить данные
                    Cheques.Delete(cheq);*/
            /*if (Goods != null)
                foreach (Good good in Task.Run(() => Goods.GetAsync()).Result) //Если нужно очистить данные
                    Goods.Delete(good);*/
            Goods.Create(new Database.Good("Хлеб", 35));
            Goods.Create(new Database.Good("Шоколад", 120));
            Goods.Create(new Database.Good("Пельмени", 309.2f));
            Goods.Create(new Database.Good("Колбаса", 225.1f));
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
