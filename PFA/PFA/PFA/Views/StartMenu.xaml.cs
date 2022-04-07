using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartMenu : ContentPage
    {
        public StartMenu()
        {
            InitializeComponent();
        }
        async void Login(object sender, EventArgs e)
        {
            await LogIn.ScaleTo(0.9, 50);
            await LogIn.ScaleTo(1, 50);
            await Navigation.PushAsync(new Login());
        }
        async void Signin(object sender, EventArgs e)
        {
            await SignIn.ScaleTo(0.9, 50);
            await SignIn.ScaleTo(1, 50);
            await Navigation.PushAsync(new Signin());
        }
    }
}