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

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cheques : ContentPage
    {
        public ICommand SharePostCommand { protected set; get; }
        public Cheques()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
        }
        void OpenCheque(object sender, EventArgs e)
        {
        }
        void CloseCheque(object sender, EventArgs e)
        {
        }
        async void CreateCheque(object sender, EventArgs e)
        {
            await Add.ScaleTo(0.9, 50);
            await Add.ScaleTo(1, 50);
            string result = await DisplayPromptAsync("Новый чек", "Имя чека", "Добавить", "Отмена");
            if (result != null)
            {
                if (result.Length != 0)
                {
                    await App.Cheques.Create(new Database.Cheque(result, DateTime.Today));
                    Cheque cheq = Task.Run(() => App.Cheques.GetAsync()).Result.Last();
                    cheq.goods.Add(new GoodsForCheque("Печеньки", 120, 5, cheq.id));
                    cheq.goods.Add(new GoodsForCheque("Вода", 12, 8, cheq.id));
                    cheq.goods.Add(new GoodsForCheque("Торт с клубникой", 520, 10, cheq.id));
                    cheq.CalculatePrice();
                    cheq.SetGoods();
                    await App.Cheques.Update(cheq);
                    BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
                }
                else
                {
                    await App.Cheques.Create(new Database.Cheque("Новый чек", DateTime.Today));
                    BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetAsync());
                }
            }

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