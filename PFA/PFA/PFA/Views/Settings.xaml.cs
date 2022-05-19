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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            User.Text = "Пользователь\n" + App.Current.Properties["user"];
        }
        async void ChangeUser(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Смена пользователя", "Вы действительно хотите выйти из текущего аккаунта?", "Выйти", "Отмена");
            if (result)
            {
                App.Current.Properties["user"] = "";
                App.Current.MainPage = new NavigationPage(new StartMenu());
            }
        }
        async void GoBack(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
        async void GotoGoodDatabase(object sender, EventArgs e)
        {
            await GoodDatabase.ScaleTo(0.9, 50);
            await GoodDatabase.ScaleTo(1, 50);
            await Navigation.PushAsync(new GoodsBase());
        }
    }
}