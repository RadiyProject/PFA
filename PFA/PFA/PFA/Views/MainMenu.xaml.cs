﻿using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PFA.Views
{
    public partial class MainMenu : ContentPage
    {

        public MainMenu()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
        async void OnChequeClicked(object sender, EventArgs e)
        {
            await Cheques.ScaleTo(0.9, 50);
            await Cheques.ScaleTo(1, 50);
            await Navigation.PushAsync(new Cheques());
        }
        async void OnBudgetClicked(object sender, EventArgs e)
        {
            await Budget.ScaleTo(0.9, 50);
            await Budget.ScaleTo(1, 50);
            await Navigation.PushAsync(new Budget());
        }
        async void OnInfoBlockClicked(object sender, EventArgs e)
        {
            await InfoBlock.ScaleTo(0.9, 50);
            await InfoBlock.ScaleTo(1, 50);
            await Navigation.PushAsync(new InfoBlock());
        }
        async void OnQuitClicked(object sender, EventArgs e)
        {
            await Quit.ScaleTo(0.9, 50);
            await Quit.ScaleTo(1, 50);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Settings.ScaleTo(0.9, 50);
            await Settings.ScaleTo(1, 50);
            await Navigation.PushAsync(new Settings());
        }
        async void OnStatisticsClicked(object sender, EventArgs e)
        {
            await Statistics.ScaleTo(0.9, 50);
            await Statistics.ScaleTo(1, 50);
            await Navigation.PushAsync(new Statistics());
        }
    }
}