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
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
        async void NewCheque(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            if (popup_Name.Text == null)
                popup_Name.Text = "Новый чек";
            Database.Cheque cheq = new Database.Cheque(popup_Name.Text, DateTime.Today);
            cheq.totalPriceText = cheq.totalPrice + "р.";
            cheq.dateText = DateTime.Today.ToString("d");
            Task t1 = Task.Run(() => App.server.AddCheque(cheq.name, cheq.date, cheq.dateText, cheq.totalPrice,
                (string)App.Current.Properties["user"], cheq.goodsString, cheq.isOpened, cheq.isClosed, cheq.colFirst, 
                cheq.colSecond, cheq.colThird, cheq.colFourth, "", null));
            await Task.WhenAll(t1);
            await page.Refresh();
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
    }
}