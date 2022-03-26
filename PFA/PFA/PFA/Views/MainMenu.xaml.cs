using System;
using System.IO;
using Xamarin.Forms;

namespace PFA.Views
{
    public partial class MainMenu : ContentPage
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
        }
        void OnMenuItemClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AboutPage();
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
        }
    }
}