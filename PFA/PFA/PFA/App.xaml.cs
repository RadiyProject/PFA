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

namespace PFA
{
    public partial class App : Application
    {
        //TcpClient clientSocket = new TcpClient();
        //NetworkStream serverStream;
        static DBCheque cheques;
        static DBGood goods;
        static DBBudget budget;
        static DBCategory categories;
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
            /*if (Cheques != null)
                foreach (Cheque cheq in Task.Run(() => Cheques.GetAsync()).Result) //Если нужно очистить данные
                    Cheques.Delete(cheq);*/
            /*if (Goods != null)
                foreach (Good good in Task.Run(() => Goods.GetAsync()).Result) //Если нужно очистить данные
                    Goods.Delete(good);*/
            /*clientSocket.Connect("192.168.100.6", 8888);
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[4096];
            int bytesRead = serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream, 0, bytesRead);*/
            //foreach (Database.Budget good in Task.Run(() => Budget.GetAsync()).Result) //Если нужно очистить данные
            //Budget.Delete(good);
            if (Task.Run(() => Budget.GetAsync()).Result.Count == 0)
                Budget.Create(new Database.Budget(0));
            if (Task.Run(() => Categories.GetAsync()).Result.Count != 4)
            {
                if (Task.Run(() => Categories.GetAsync()).Result.Count != 0)
                    foreach(Category category in Task.Run(() => Categories.GetAsync()).Result)
                        Categories.Delete(category);
                Categories.Create(new Category("Еда"));
                Categories.Create(new Category("Одежда"));
                Categories.Create(new Category("Транспорт"));
                Categories.Create(new Category("Развлечения"));
            }
            Database.Budget budget = Task.Run(() => Budget.GetAsync()).Result.Last();
            NavigationPage page;
            if (budget.userId == null ||
                budget.userId.Trim() != string.Empty)
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
