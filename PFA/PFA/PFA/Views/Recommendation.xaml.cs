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
    public partial class Recommendation : ContentPage
    {
        ServiceReference1.Recomendation recom;
        public Recommendation()
        {
            InitializeComponent();
        }
        public Recommendation(ServiceReference1.Recomendation recom)
        {
            InitializeComponent();
            this.recom = recom;
            Name.Text = recom.name;
            Description.Text = recom.description;
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}