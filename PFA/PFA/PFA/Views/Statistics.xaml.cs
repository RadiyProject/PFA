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
    public partial class Statistics : ContentPage
    {
        public Statistics()
        {
            InitializeComponent();
            newcollec();
        }
        public async void newcollec()
        {
            name_colec.ItemsSource = await App.Cheques.GetAsync();
        }

        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.9, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PushAsync(new MainMenu());
        }
    }
}