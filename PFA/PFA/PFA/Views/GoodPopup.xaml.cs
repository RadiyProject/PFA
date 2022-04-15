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
        public GoodPopup()
        {
            InitializeComponent();
        }

        async void ClosePopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        async void new_prod(object sender, EventArgs e)
        {
            if (popup_Name.Text != null & popup_Price.Text != null)
            {
                await Navigation.PushAsync(new GoodsBase(popup_Name.Text, float.Parse(popup_Price.Text)));
                await PopupNavigation.Instance.PopAllAsync();
            }
            if (popup_Name.Text != null & popup_Price.Text == null)
            {
                await Navigation.PushAsync(new GoodsBase(popup_Name.Text, 0));
                await PopupNavigation.Instance.PopAllAsync();
            }
            if (popup_Price.Text != null & popup_Name.Text == null)
            {
                await Navigation.PushAsync(new GoodsBase("Безымянный", float.Parse(popup_Price.Text)));
                await PopupNavigation.Instance.PopAllAsync();
            }
            if (popup_Price.Text == null & popup_Name.Text == null)
            {
                await Navigation.PushAsync(new GoodsBase("Безымянный", 0));
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
    }
}