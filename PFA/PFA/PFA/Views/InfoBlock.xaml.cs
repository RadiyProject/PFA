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
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
        async void OpenFiveAdvices(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new FiveAdvices());
        }
    }
}