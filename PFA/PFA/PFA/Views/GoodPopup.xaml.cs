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
            picker.ItemsSource = await App.Categories.GetAsync();
        }
        async void ClosePopup(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.5, 50);
            await button.ScaleTo(1, 50);
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
                category = 0;
            await App.Goods.Create(new Database.Good(popup_Name.Text, price, category));
            await page.Refresh();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}