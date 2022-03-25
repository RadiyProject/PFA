using PFA.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PFA.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}