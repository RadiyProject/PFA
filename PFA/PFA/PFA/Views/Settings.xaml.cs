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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }
        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
        async void GotoGoodDatabase(object sender, EventArgs e)
        {
            await GoodDatabase.ScaleTo(0.9, 50);
            await GoodDatabase.ScaleTo(1, 50);
            await Navigation.PushAsync(new GoodsBase());
        }
    }
}