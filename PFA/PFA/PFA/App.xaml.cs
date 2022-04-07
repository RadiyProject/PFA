using PFA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFA
{
    public partial class App : Application
    {

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
