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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Refresh();
        }
        private async Task ResetOpenedGoods()
        {
            ServiceReference1.Good[] goods = null;
            Task t1 = Task.Run(() => goods = App.server.GetAllGoods((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (goods != null)
            {
                foreach (ServiceReference1.Good good in goods)
                {
                    good.isOpened = false;
                    good.isClosed = true;
                    good.colFirst = 0.63f;
                    good.colSecond = 0.3f;
                    good.colThird = 0.07f;
                    t1 = Task.Run(() => App.server.UpdateGood(good));
                    await Task.WhenAll(t1);
                }
            }
        }
        async void ActWithGood(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.8, 50);
            await button.ScaleTo(1, 50);
            ServiceReference1.Good good = (ServiceReference1.Good)button.CommandParameter;
            if (good != null)
            {
                good.isOpened = good.isClosed;
                good.isClosed = !good.isClosed;
                if (good.isOpened)
                {
                    good.colFirst = 0.93f;
                    good.colSecond = 0f;
                    good.colThird = 0.07f;
                }
                else
                {
                    good.colFirst = 0.63f;
                    good.colSecond = 0.3f;
                    good.colThird = 0.07f;
                }
                Task t1 = Task.Run(() => App.server.UpdateGood(good));
                await Task.WhenAll(t1);
            }
            if (button.RotationX == 0)
                await button.RotateXTo(180, 200);
            else
                await button.RotateXTo(0, 200);
            await RefreshData ();
        }
        public async Task Refresh()
        {
            await ResetOpenedGoods();
            await RefreshData();
        }
        void OnNameFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Good good = (ServiceReference1.Good)entry.ReturnCommandParameter;
            entry.Text = good.name;
        }
        async void OnNameUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Good good = (ServiceReference1.Good)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (good.name != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    good.name = entry.Text;
                    good.nameWithPrice = good.name + " " + good.priceText;
                    Task t1 = Task.Run(() => App.server.UpdateGood(good));
                    await Task.WhenAll(t1);

                    await RefreshData ();
                }
                else
                    entry.Text = good.name;
            }
        }
        async void CategoryChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            Grid parent = (Grid)(picker.Parent);
            Button button = new Button();
            foreach (Object obj in parent.Children)
                if (TypeDescriptor.GetClassName(obj) == TypeDescriptor.GetClassName(button))
                    button = (Button)obj;
            ServiceReference1.Good good = (ServiceReference1.Good)button.CommandParameter;
            good.category = ((ServiceReference1.Category)picker.SelectedItem).id;
            string name = "";
            Task t1 = Task.Run(() => name = App.server.GetCategoryName(good.category));
            await Task.WhenAll(t1);
            good.selected = name;
            t1 = Task.Run(() => App.server.UpdateGood(good));
            await Task.WhenAll(t1);

            await RefreshData ();
        }
        void OnPriceFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Good good = (ServiceReference1.Good)entry.ReturnCommandParameter;
            entry.Text = good.price.ToString();
        }
        async void OnPriceUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Good good = (ServiceReference1.Good)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (entry.Text != null && entry.Text.Trim() != string.Empty && good.price != float.Parse(entry.Text))
                {
                    good.price = float.Parse(entry.Text);
                    good.priceText = good.price + "р.";
                    good.nameWithPrice = good.name + " " + good.priceText;
                    entry.Text = good.priceText.ToString();
                    Task t1 = Task.Run(() => App.server.UpdateGood(good));
                    await Task.WhenAll(t1);

                    await RefreshData();
                }
                else
                    entry.Text = good.priceText.ToString();
            }
        }
        async void delete_button(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.ScaleTo(0.9, 50);
            await button.ScaleTo(1, 50);
            bool result = await DisplayAlert("Удаление чека", "Вы действительно хотите удалить чек?", "Удалить", "Отмена");
            if (result)
            {
                ServiceReference1.Good good = (ServiceReference1.Good)button.CommandParameter;
                if (good != null)
                {
                    Task t1 = Task.Run(() => App.server.DeleteGood(good.id));
                    await Task.WhenAll(t1);
                }
                await RefreshData ();
            }
        }
        async Task RefreshData()
        {
            ServiceReference1.Good[] goods = null;
            Task t1 = Task.Run(() => goods = App.server.GetAllGoods((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            BindableLayout.SetItemsSource(GoodsStack, goods);
        }
        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}