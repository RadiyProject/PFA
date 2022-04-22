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
    public partial class InfoBlock : ContentPage
    {
        public InfoBlock()
        {
            InitializeComponent();
        }
        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.9, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PushAsync(new MainMenu());
        }
        async void OpenFiveAdvices(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new FiveAdvices());
        }
    }
}