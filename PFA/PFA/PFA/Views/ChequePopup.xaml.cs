using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChequePopup
    {
        Cheques page;
        public ChequePopup(Cheques page)
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
        async void NewCheque(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            if (popup_Name.Text == null)
                popup_Name.Text = "Новый чек";
            await App.Cheques.Create(new Database.Cheque(popup_Name.Text, DateTime.Today));
            await page.Refresh();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}