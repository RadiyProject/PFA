using Microcharts;
using PFA.Database;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : ContentPage
    {
        public DateTime first;
        public DateTime last;
        string[] categoriesNames;
        float[] categories;
        public Statistics()
        {
            InitializeComponent();
            first = DateTime.Today;
            last = DateTime.Today;
            GetChart();
        }
        void GetChart()
        {
            int categoriesCount = Task.Run(() => App.Categories.GetAsync()).Result.Count;
            List<Cheque> cheques = Task.Run(async () => await App.Cheques.GetAsync()).Result;
            float sum = 0;
            categories = new float[categoriesCount + 1];
            categoriesNames = new string[categoriesCount + 1];
            categoriesNames[categories.Length - 1] = "Без категории";
            List<Category> cats = Task.Run(() => App.Categories.GetAsync()).Result;
            int j = 0;
            foreach (Category cat in cats)
            {
                categoriesNames[j] = cat.name;
                j++;
            }
            foreach (Cheque cheque in cheques)
            {
                sum += cheque.totalPrice;
                cheque.goods = cheque.goodsN;
                if (cheque.goods != null)
                    foreach (GoodsForCheque good in cheque.goods)
                    {
                        Category category = GetCategory(good.category);
                        if (category != null)
                            categories[GetIndex(good.category, cats)] += good.amount * good.price;
                        else
                            categories[categories.Length - 1] += good.amount * good.price;
                    }
            }
            List<Entry> entries = new List<Entry>();
            Random random = new Random();
            for (int i = 0; i < categories.Length; i++)
            {
                Entry entry = new Entry(categories[i]);
                entry.Label = categoriesNames[i];
                entry.ValueLabel = categories[i].ToString() + "р.";
                entry.Color = SKColor.Parse(String.Format("#{0:X6}", random.Next(0x1000000)));
                entry.ValueLabelColor = entry.Color;
                entries.Add(entry);
            }
            Chart.HeightRequest = 50 * categories.Length;
            Chart.Chart = new DonutChart { Entries = entries, BackgroundColor = SKColors.Transparent, LabelTextSize = 45 };
        }
        int GetIndex(int id, List<Category> categories)
        {
            int i = 0;
            foreach (Category category in categories) {
                if (category.id == id)
                    return i;
                i++;
            }
            return -1;
        }
        Category GetCategory(int id)
        {
            List<Category> categories = Task.Run(() => App.Categories.GetAsync()).Result;
            foreach (Category category in categories)
                if (category.id == id)
                    return category;
            return null;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Refresh();
        }
        async void OnMinDateSelected(object sender, DateChangedEventArgs args)
        {
            first = ((DatePicker)sender).Date;
            await Refresh();
        }
        async void OnMaxDateSelected(object sender, DateChangedEventArgs args)
        {
            last = ((DatePicker)sender).Date;
            await Refresh();
        }
        public async Task Refresh()
        {
            BindableLayout.SetItemsSource(ChequesStack, await App.Cheques.GetWithIntervalAsync(first, last));
        }

        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}