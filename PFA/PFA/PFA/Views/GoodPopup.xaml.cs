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
            if (popup_Name.Text != null & popup_Price.Text != null)
                await App.Goods.Create(new Database.Good(popup_Name.Text, float.Parse(popup_Price.Text)));
            if (popup_Name.Text != null & popup_Price.Text == null)
                await App.Goods.Create(new Database.Good(popup_Name.Text, 0));
            if (popup_Price.Text != null & popup_Name.Text == null)
                await App.Goods.Create(new Database.Good("Безымянный", float.Parse(popup_Price.Text)));
            if (popup_Price.Text == null & popup_Name.Text == null)
                await App.Goods.Create(new Database.Good("Безымянный", 0));
            page.Refresh();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}