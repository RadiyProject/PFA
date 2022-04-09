using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpnado.Shades;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            name_colec.ItemsSource = await App.Database_receipt.GetReceiptAsync();
        }
        void Close_card_information(object sender, EventArgs e)
        {


        }
        void Open_information(object sender, EventArgs e)
        {



        }
        async void new_reseipt(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Новый чек", "Имя чека");
            if (result.Length != 0)
            {
                await App.Database_receipt.CreateReceipt(new Database.Receipt
                {
                    reseipt_name = result,
                    reseipt_date = DateTime.Today.ToString("d"),
                    reseipt_total = 1500
                });
                name_colec.ItemsSource = await App.Database_receipt.GetReceiptAsync();
            }

        }
        void open_card_full(object sender, EventArgs e)
        {

        }

        async void delete_button(object sender, EventArgs e)
        {
            var receipt = (Database.Receipt)BindingContext;
            await App.Database_receipt.DeleteReceipt(receipt);

        }
    }
}