using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using PFA.Database;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;


namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodsBase : ContentPage
    {
        public GoodsBase()
        {
            InitializeComponent();
        }
        private async void ShowNewUserPopup(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            await PopupNavigation.Instance.PushAsync(new GoodPopup(this));
        }
        public GoodsBase(string name, float price)
        {
            InitializeComponent();
            NewProductToDB(name, price);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            name_colec.ItemsSource = await App.Goods.GetAsync();
        }
        public async void Refresh()
        {
            name_colec.ItemsSource = await App.Goods.GetAsync();
        }
        public async void NewProductToDB(string result, float result2)
        {
            await App.Goods.Create(new Database.Good(result, result2));
            name_colec.ItemsSource = await App.Goods.GetAsync();

        }
        async void delete_button(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.ScaleTo(0.9, 50);
            await button.ScaleTo(1, 50);
            bool result = await DisplayAlert("Удаление чека", "Вы действительно хотите удалить чек?", "Удалить", "Отмена");
            if (result)
            {
                Good good = (Good)button.CommandParameter;
                if (good != null)
                    await App.Goods.Delete(good);
                name_colec.ItemsSource = await App.Goods.GetAsync();
            }
        }
        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}