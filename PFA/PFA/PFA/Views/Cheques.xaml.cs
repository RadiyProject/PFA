using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpnado.Shades;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PFA.Database;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Newtonsoft.Json;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cheques : ContentPage
    {
        public Cheques()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Refresh();
        }
        private async Task ResetOpenedCheques()
        {
            ServiceReference1.Cheque[] cheques = null;
            Task t1 = Task.Run(() => cheques = App.server.GetAllCheque((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (cheques != null)
            {
                foreach (ServiceReference1.Cheque cheq in cheques)
                {
                    cheq.isOpened = false;
                    cheq.isClosed = true;
                    cheq.colFirst = 0.3f;
                    cheq.colSecond = 0.35f;
                    cheq.colThird = 0.28f;
                    cheq.colFourth = 0.07f;
                    t1 = Task.Run(() => App.server.UpdateCheque(cheq));
                    await Task.WhenAll(t1);
                }
            }
        }
        public async Task Refresh()
        {
            await ResetOpenedCheques();
            await RefreshData();
        }
        void OnNameFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Cheque cheq = (ServiceReference1.Cheque)entry.ReturnCommandParameter;
            entry.Text = cheq.name;
        }
        async void OnNameUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Cheque cheq = (ServiceReference1.Cheque)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (cheq.name != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    cheq.name = entry.Text;
                    Task t1 = Task.Run(() => App.server.UpdateCheque(cheq));
                    await Task.WhenAll(t1);
                    
                    await RefreshData();
                }
                else
                    entry.Text = cheq.name;
            }
        }
        async void ActWithCheque(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.8, 50);
            await button.ScaleTo(1, 50);
            ServiceReference1.Cheque cheq = (ServiceReference1.Cheque)button.CommandParameter;
            if (cheq != null)
            {
                cheq.isOpened = cheq.isClosed;
                cheq.isClosed = !cheq.isClosed;
                if (cheq.isOpened)
                {
                    cheq.colFirst = 0.93f;
                    cheq.colSecond = 0f;
                    cheq.colThird = 0f;
                    cheq.colFourth = 0.07f;
                }
                else
                {
                    cheq.colFirst = 0.3f;
                    cheq.colSecond = 0.35f;
                    cheq.colThird = 0.28f;
                    cheq.colFourth = 0.07f;
                }
                Task t1 = Task.Run(() => App.server.UpdateCheque(cheq));
                await Task.WhenAll(t1);
            }
            if (button.RotationX == 0)
                await button.RotateXTo(180, 200);
            else
                await button.RotateXTo(0, 200);
            await RefreshData();
        }
        private async void ShowChequePopup(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            await PopupNavigation.Instance.PushAsync(new ChequePopup(this));
        }
        async void DeleteCheque(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.ScaleTo(0.9, 50);
            await button.ScaleTo(1, 50);
            bool result = await DisplayAlert("Удаление чека", "Вы действительно хотите удалить чек?", "Удалить", "Отмена");
            if (result)
            {
                ServiceReference1.Cheque cheque = (ServiceReference1.Cheque)button.CommandParameter;
                if (cheque != null)
                {
                    Task t1 = Task.Run(() => App.server.DeleteCheque(cheque.id));
                    await Task.WhenAll(t1);
                }
                await RefreshData();
            }
        }
        async void AddGood(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            Picker picker = (Picker)button.CommandParameter;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                ServiceReference1.GoodsForCheque good = (ServiceReference1.GoodsForCheque)picker.ItemsSource[selectedIndex];
                ServiceReference1.Cheque cheque = null;
                Task t1 = Task.Run(() => cheque = App.server.GetCheque(good.id));
                await Task.WhenAll(t1);
                bool canAdd = true;
                if (cheque.goods != null && cheque.goods.Length > 0)
                    foreach (ServiceReference1.GoodsForCheque temp in cheque.goods)
                        if (temp.name == good.name && temp.price == good.price)
                            canAdd = false;
                if (canAdd)
                {
                    if (cheque.goods != null)
                    {
                        ServiceReference1.GoodsForCheque[] temps = new ServiceReference1.GoodsForCheque[cheque.goods.Length + 1];
                        int i = 0;
                        foreach (ServiceReference1.GoodsForCheque temp in cheque.goods)
                        {
                            temps[i] = temp;
                            i++;
                        }
                        temps[cheque.goods.Length] = good;
                        cheque.goods = temps;
                    }
                    else
                    {
                        cheque.goods = new ServiceReference1.GoodsForCheque[1];
                        cheque.goods[0] = good;
                    }
                    if (cheque.goods != null && cheque.goods.Length > 0)
                    {
                        cheque.totalPrice = 0;
                        for (int i = 0; i < cheque.goods.Length; i++)
                            if (cheque.goods[i].amount > 0)
                                cheque.totalPrice += cheque.goods[i].price * cheque.goods[i].amount;
                    }
                    cheque.totalPriceText = cheque.totalPrice + "р.";
                    t1 = Task.Run(() => App.server.UpdateCheque(cheque));
                    await Task.WhenAll(t1);
                    await RefreshData();
                }
            }
        }
        async void IncreaseVal(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            ServiceReference1.GoodsForCheque good = (ServiceReference1.GoodsForCheque)button.CommandParameter;
            if (good != null)
            {
                ServiceReference1.Cheque cheque = null;
                Task t1 = Task.Run(() => cheque = App.server.GetCheque(good.id));
                await Task.WhenAll(t1);
                foreach (ServiceReference1.GoodsForCheque goodEd in cheque.goods)
                    if (goodEd.idGFC == good.idGFC)
                    {
                        goodEd.amount++;
                        goodEd.amountText = "x" + goodEd.amount;
                        goodEd.priceText = goodEd.price * goodEd.amount + "р.";
                    }
                if (cheque.goods != null && cheque.goods.Length > 0)
                {
                    cheque.totalPrice = 0;
                    for (int i = 0; i < cheque.goods.Length; i++)
                        if (cheque.goods[i].amount > 0)
                            cheque.totalPrice += cheque.goods[i].price * cheque.goods[i].amount;
                }
                cheque.totalPriceText = cheque.totalPrice + "р.";
                t1 = Task.Run(() => App.server.UpdateCheque(cheque));
                await Task.WhenAll(t1);
            }
            await RefreshData();
        }
        async void DecreaseVal(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            ServiceReference1.GoodsForCheque good = (ServiceReference1.GoodsForCheque)button.CommandParameter;
            if (good != null)
            {
                ServiceReference1.Cheque cheque = null;
                Task t1 = Task.Run(() => cheque = App.server.GetCheque(good.id));
                await Task.WhenAll(t1);
                int deleteCount = 0;
                foreach (ServiceReference1.GoodsForCheque goodEd in cheque.goods)
                    if (goodEd.idGFC == good.idGFC)
                    {
                        goodEd.amount--;
                        goodEd.amountText = "x" + goodEd.amount;
                        goodEd.priceText = goodEd.price * goodEd.amount + "р.";
                        if (goodEd.amount <= 0)
                            deleteCount++;
                    }
                if (deleteCount > 0)
                {
                    ServiceReference1.GoodsForCheque[] temps = new ServiceReference1.GoodsForCheque[cheque.goods.Length - deleteCount];
                    int i = 0;
                    foreach (ServiceReference1.GoodsForCheque temp in cheque.goods)
                    {
                        if (temp.amount > 0)
                        {
                            temps[i] = temp;
                            i++;
                        }
                    }
                    cheque.goods = temps;
                }
                if (cheque.goods != null && cheque.goods.Length > 0)
                {
                    cheque.totalPrice = 0;
                    for (int i = 0; i < cheque.goods.Length; i++)
                        if (cheque.goods[i].amount > 0)
                            cheque.totalPrice += cheque.goods[i].price * cheque.goods[i].amount;
                }
                if (cheque.goods == null || cheque.goods.Length == 0)
                    cheque.totalPrice = 0;
                cheque.totalPriceText = cheque.totalPrice + "р.";
                t1 = Task.Run(() => App.server.UpdateCheque(cheque));
                await Task.WhenAll(t1);
            }
            await RefreshData();
        }
        async Task RefreshData()
        {
            ServiceReference1.Cheque[] cheques = null;
            Task t1 = Task.Run(() => cheques = App.server.GetAllCheque((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            BindableLayout.SetItemsSource(ChequesStack, cheques);
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}