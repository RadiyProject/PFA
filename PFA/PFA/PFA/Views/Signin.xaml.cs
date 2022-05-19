﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ServiceReference1;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signin : ContentPage
    {
        private bool isHidden = true;
        private bool isHiddenConfirm = true;
        public Signin()
        {
            InitializeComponent();
        }
        async void Authorization(object sender, EventArgs e)
        {
            await Authorize.ScaleTo(0.9, 50);
            await Authorize.ScaleTo(1, 50);
            if (Pass.Text != null && PassConfirm.Text != null && Username.Text != null)
            {
                if (Pass.Text != PassConfirm.Text)
                {
                    PassError.IsVisible = true;
                }
                else
                {
                    PassError.IsVisible = false;
                    int result = 0;
                    Task t1 = Task.Run(() => result = App.server.CheckLogin(Username.Text));
                    await Task.WhenAll(t1);
                    if (result != 1)
                    {
                        UserError.IsVisible = true;
                    }
                    else
                    {
                        UserError.IsVisible = false;
                        string res = "";
                        t1 = Task.Run(() => res = App.server.AddUser(Username.Text, Pass.Text));
                        await Task.WhenAll(t1);
                        if (res == "1")
                        {
                            App.Current.Properties["user"] = Username.Text;
                            App.Current.MainPage = new NavigationPage(new MainMenu());
                        }
                    }
                }
            }
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
        void ShowPas(object sender, EventArgs e)
        {
            isHidden = !isHidden;
            if (isHidden)
            {
                Circle.BackgroundColor = Xamarin.Forms.Color.FromHex("#00FFFFFF");
                Pass.IsPassword = true;
            }
            else
            {
                Circle.BackgroundColor = Xamarin.Forms.Color.FromHex("#528C83");
                Pass.IsPassword = false;
            }
        }
        void ShowPasConfirm(object sender, EventArgs e)
        {
            isHiddenConfirm = !isHiddenConfirm;
            if (isHiddenConfirm)
            {
                CircleConfirm.BackgroundColor = Xamarin.Forms.Color.FromHex("#00FFFFFF");
                PassConfirm.IsPassword = true;
            }
            else
            {
                CircleConfirm.BackgroundColor = Xamarin.Forms.Color.FromHex("#528C83");
                PassConfirm.IsPassword = false;
            }
        }
        void OnFlexClicked(object sender, EventArgs e)
        {
            if (Pass.IsFocused)
                Pass.Unfocus();
            Pass.Focus();
        }
        void OnFlexClickedConfirm(object sender, EventArgs e)
        {
            if (PassConfirm.IsFocused)
                PassConfirm.Unfocus();
            PassConfirm.Focus();
        }
    }
}