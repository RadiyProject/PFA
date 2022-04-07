using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private bool isHidden = true;
        public Login()
        {
            InitializeComponent();
        }
        async void Authorization(object sender, EventArgs e)
        {
            await Authorize.ScaleTo(0.9, 50);
            await Authorize.ScaleTo(1, 50);
            await Navigation.PushAsync(new MainMenu());
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.9, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PushAsync(new StartMenu());
        }
        void ShowPas(object sender, EventArgs e)
        {
            isHidden = !isHidden;
            if (isHidden)
            {
                Circle.BackgroundColor = Xamarin.Forms.Color.FromHex("#00FFFFFF");
                Pass.IsPassword = true;
            }
            else
            {
                Circle.BackgroundColor = Xamarin.Forms.Color.FromHex("#528C83");
                Pass.IsPassword = false;
            }
        }
    }
}