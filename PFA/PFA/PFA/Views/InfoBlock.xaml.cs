using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Refresh();
        }
        public async Task Refresh()
        {
            ServiceReference1.Recomendation[] recomms = null;
            Task t1 = Task.Run(() => recomms = App.server.GetAllRecomendations());
            await Task.WhenAll(t1);
            BindableLayout.SetItemsSource(RecommendationsStack, recomms);
        }
        async void OpenFiveAdvices(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new FiveAdvices());
        }
        async void OnRecom(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid parent = (Grid)(grid.Parent);
            Button button = new Button();
            foreach (Object obj in parent.Children)
                if (TypeDescriptor.GetClassName(obj) == TypeDescriptor.GetClassName(button))
                    button = (Button)obj;
            ServiceReference1.Recomendation recom = (ServiceReference1.Recomendation)button.CommandParameter;
            await Navigation.PushAsync(new Recommendation(recom));
        }
    }
}