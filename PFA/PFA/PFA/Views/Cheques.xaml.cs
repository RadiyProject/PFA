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
            name_colec.ItemsSource = await App.Cheques.GetAsync();
        }
        void Close_card_information(object sender, EventArgs e)
        {


        }
        void Open_information(object sender, EventArgs e)
        {



        }
        async void new_reseipt(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Новый чек", "Имя чека", "Добавить", "Отмена");
            if (result != null)
            {
                if (result.Length != 0)
                {
                    await App.Cheques.Create(new Database.Cheque(result, DateTime.Today));
                    name_colec.ItemsSource = await App.Cheques.GetAsync();
                }
                else
                {
                    await App.Cheques.Create(new Database.Cheque());
                    name_colec.ItemsSource = await App.Cheques.GetAsync();
                }
            }

        }
        void open_card_full(object sender, EventArgs e)
        {

        }

        async void delete_button(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Cheque cheque = (Cheque)button.CommandParameter;
            if (cheque != null)
                await App.Cheques.Delete(cheque);
            name_colec.ItemsSource = await App.Cheques.GetAsync();
        }
    }
}