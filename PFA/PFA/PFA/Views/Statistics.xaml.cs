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
        }
        async Task GetChart()
        {
            ServiceReference1.Category[] categs = null;
            Task t1 = Task.Run(() => categs = App.server.GetCategories());
            await Task.WhenAll(t1);
            int categoriesCount = categs.Length;
            ServiceReference1.Cheque[] cheques = null;
            t1 = Task.Run(() => cheques = App.server.GetAllCheque((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            float sum = 0;
            categories = new float[categoriesCount + 1];
            categoriesNames = new string[categoriesCount + 1];
            categoriesNames[categories.Length - 1] = "Без категории";
            int j = 0;
            foreach (ServiceReference1.Category cat in categs)
            {
                categoriesNames[j] = cat.name;
                j++;
            }
            foreach (ServiceReference1.Cheque cheque in cheques)
            {
                sum += cheque.totalPrice;
                if (cheque.goods != null)
                    foreach (ServiceReference1.GoodsForCheque good in cheque.goods)
                    {
                        sum += cheque.totalPrice;
                        ServiceReference1.Category category = Task.Run(async() => await GetCategory(good.category)).Result;
                        if (category != null)
                            categories[GetIndex(good.category, categs)] += good.amount * good.price;
                        else
                            categories[categories.Length - 1] += good.amount * good.price;
                    }
            }
            List<Entry> entries = new List<Entry>();
            Random random = new Random();
            int categoryType = 0;
            float val = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                Entry entry = new Entry(categories[i]);
                entry.Label = categoriesNames[i];
                entry.TextColor = SKColors.Black;
                entry.ValueLabel = categories[i].ToString() + "р.";
                if (categories[i] > val)
                {
                    categoryType = i;
                    val = categories[i];
                }
                entry.Color = SKColor.Parse(String.Format("#{0:X6}", random.Next(0x1000000)));
                entry.ValueLabelColor = entry.Color;
                entries.Add(entry);
            }
            if (categoryType != categories.Length - 1)
            {
                j = 0;
                foreach (ServiceReference1.Category cat in categs)
                {
                    if (categoriesNames[categoryType] == cat.name)
                    {
                        categoriesNames[j] = cat.name;
                        categoryType = cat.id;
                        break;
                    }
                    j++;
                }
            }
            else
                categoryType = 0;
            Chart.HeightRequest = 50 * categories.Length;
            Chart.Chart = new DonutChart { Entries = entries, BackgroundColor = SKColors.Transparent, LabelTextSize = 35,  HoleRadius = 2, };
            ServiceReference1.Recomendation recom = null;
            t1 = Task.Run(() => recom = App.server.GetRecomendationCathRandom(categoryType));
            await Task.WhenAll(t1);
            if (recom != null)
                Recommendation.Text = recom.description;
        }
        int GetIndex(int id, ServiceReference1.Category[] categories)
        {
            int i = 0;
            foreach (ServiceReference1.Category category in categories) {
                if (category.id == id)
                    return i;
                i++;
            }
            return -1;
        }
        async Task<ServiceReference1.Category> GetCategory(int id)
        {
            ServiceReference1.Category[] categories = App.server.GetCategories();
            Task t1 = Task.Run(() => categories = App.server.GetCategories());
            await Task.WhenAll(t1);
            foreach (ServiceReference1.Category category in categories)
                if (category.id == id)
                    return category;
            return null;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            first = DateTime.Today.AddDays(- 1);
            last = DateTime.Today.AddDays(1);
            await GetChart();
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
            ServiceReference1.Cheque[] cheques = null;
            Task t1 = Task.Run(() => cheques = App.server.GetChequeInInterval((string)App.Current.Properties["user"], first, last));
            await Task.WhenAll(t1);
            BindableLayout.SetItemsSource(ChequesStack, cheques);
        }

        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}