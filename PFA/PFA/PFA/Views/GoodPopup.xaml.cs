using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using PFA.Database;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodPopup 
    {
        GoodsBase page;
        public GoodPopup(GoodsBase page)
        {
            InitializeComponent();
            this.page = page;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ServiceReference1.Category[] categories = null;
            Task t1 = Task.Run(() => categories = App.server.GetCategories());
            await Task.WhenAll(t1);
            picker.ItemsSource = categories;
        }
        async void ClosePopup(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.5, 50);
            await button.ScaleTo(1, 50);
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
        async void new_prod(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            float price = 0;
            int category = 0;
            if (popup_Name.Text == null)
                popup_Name.Text = "Новый товар";
            if (popup_Price.Text == null || !float.TryParse(popup_Price.Text, out price)
                || price <= 0)
                price = 0;
            if (picker.SelectedItem != null)
                category = ((ServiceReference1.Category)picker.SelectedItem).id;
            Good temp = new Database.Good(popup_Name.Text, price, category);
            Task t1 = Task.Run(() => App.server.AddGood(temp.name, temp.nameWithPrice, temp.price, temp.priceText, temp.isOpened,
                temp.isClosed, temp.colFirst, temp.colSecond, temp.colThird, temp.category,
                (string)App.Current.Properties["user"], temp.selected));
            await Task.WhenAll(t1);
            await page.Refresh();
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
    }
}