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
            foreach (Cheque cheq in Task.Run(() => App.Cheques.GetAsync()).Result)
            {
                cheq.isOpened = false;
                cheq.isClosed = true;
                cheq.colFirst = 0.3f;
                cheq.colSecond = 0.35f;
                cheq.colThird = 0.28f;
                cheq.colFourth = 0.07f;
                await App.Cheques.Update(cheq);
            }
        }
        public async Task Refresh()
        {
            await ResetOpenedCheques();
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
        }
        void OnNameFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Cheque cheq = (Cheque)entry.ReturnCommandParameter;
            entry.Text = cheq.name;
        }
        async void OnNameUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Cheque cheq = (Cheque)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (cheq.name != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    cheq.name = entry.Text;
                    await App.Cheques.Update(cheq);

                    BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
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
            Cheque cheq = (Cheque)button.CommandParameter;
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
                await App.Cheques.Update(cheq);
            }
            if (button.RotationX == 0)
                await button.RotateXTo(180, 200);
            else
                await button.RotateXTo(0, 200);
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
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
                Cheque cheque = (Cheque)button.CommandParameter;
                if (cheque != null)
                    await App.Cheques.Delete(cheque);
                BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
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
                GoodsForCheque good = (GoodsForCheque)picker.ItemsSource[selectedIndex];
                Cheque cheq = Task.Run(() => App.Cheques.GetWithIdAsync(good.id)).Result.Last();
                cheq.goods = cheq.goodsN;
                bool canAdd = true;
                if (cheq.goodsN != null && cheq.goodsN.Count > 0)
                    foreach (GoodsForCheque temp in cheq.goodsN)
                        if (temp.name == good.name && temp.price == good.price)
                            canAdd = false;
                if (canAdd)
                {
                    if (cheq.goods != null)
                        cheq.goods.Add(good);
                    else
                    {
                        cheq.goods = new List<GoodsForCheque>();
                        cheq.goods.Add(good);
                    }
                    cheq.CalculatePrice();
                    cheq.SetGoods();
                    await App.Cheques.Update(cheq);
                    BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
                }
            }
        }
        async void IncreaseVal(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            GoodsForCheque good = (GoodsForCheque)button.CommandParameter;
            if (good != null)
            {
                Cheque cheq = Task.Run(() => App.Cheques.GetWithIdAsync(good.id)).Result.Last();
                List<GoodsForCheque> goods = cheq.goodsN;
                foreach (GoodsForCheque goodEd in goods)
                    if (goodEd.name == good.name && goodEd.price == good.price && goodEd.id == good.id)
                        goodEd.IncreaseAmount();
                cheq.goods = goods;
                cheq.CalculatePrice();
                cheq.SetGoods();
                await App.Cheques.Update(cheq);
            }
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
        }
        async void DecreaseVal(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            GoodsForCheque good = (GoodsForCheque)button.CommandParameter;
            if (good != null)
            {
                Cheque cheq = Task.Run(() => App.Cheques.GetWithIdAsync(good.id)).Result.Last();
                List<GoodsForCheque> goods = cheq.goodsN;
                foreach (GoodsForCheque goodEd in goods)
                    if (goodEd.name == good.name && goodEd.price == good.price && goodEd.id == good.id)
                        goodEd.DecreaseAmount();
                cheq.goods = goods;
                cheq.CalculatePrice();
                cheq.SetGoods();
                await App.Cheques.Update(cheq);
            }
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}