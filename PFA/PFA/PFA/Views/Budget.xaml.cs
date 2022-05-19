using PFA.Database;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Budget : ContentPage
    {
        public Budget()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (budget == null)
            {
                t1 = Task.Run(() => budget = App.server.AddBudget(0, false, (string)App.Current.Properties["user"], ""));
                await Task.WhenAll(t1);
                Limit.Placeholder = budget.limit.ToString() + "р.";
                await Refresh();
            }
            else
            {
                Limit.Placeholder = budget.limit.ToString() + "р.";
                await Refresh();
            }
        }
        async void ActWithTarget(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.8, 50);
            await button.ScaleTo(1, 50);
            ServiceReference1.Target target = (ServiceReference1.Target)button.CommandParameter;
            if (target != null)
            {
                ServiceReference1.Budget budget = null;
                Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
                await Task.WhenAll(t1);
                foreach (ServiceReference1.Target tar in budget.targets)
                {
                    if (tar.id == target.id)
                    {
                        tar.isOpened = tar.isClosed;
                        tar.isClosed = !tar.isClosed;
                        if (tar.isOpened)
                        {
                            tar.colFirst = 0.93f;
                            tar.colSecond = 0f;
                            tar.colThird = 0f;
                            tar.colFourth = 0.07f;
                        }
                        else
                        {
                            tar.colFirst = 0.3f;
                            tar.colSecond = 0.35f;
                            tar.colThird = 0.28f;
                            tar.colFourth = 0.07f;
                        }
                    }
                }
                t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                await Task.WhenAll(t1);
            }
            if (button.RotationX == 0)
                await button.RotateXTo(180, 200);
            else
                await button.RotateXTo(0, 200);
            await RefreshData();
        }
        void OpenCheque(object sender, EventArgs e)
        {
        }
        void CloseCheque(object sender, EventArgs e)
        {
        }
        public async Task Refresh()
        {
            await ResetOpenedTargets();
            await RefreshData();
        }
        async Task RefreshData()
        {
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            BindableLayout.SetItemsSource(BudgetStack, budget.targets);
        }
        private async Task ResetOpenedTargets()
        {
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            foreach (ServiceReference1.Target tar in budget.targets)
            {
                tar.isOpened = false;
                tar.isClosed = true;
                tar.colFirst = 0.3f;
                tar.colSecond = 0.35f;
                tar.colThird = 0.28f;
                tar.colFourth = 0.07f;
            }
            t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
            await Task.WhenAll(t1);
        }
        async void OnLimitFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (budget != null)
                entry.Text = budget.limit.ToString();
        }
        async void OnLimitUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (!entry.IsFocused && budget != null)
            {
                if (budget.limit.ToString() != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    float lim = 0;
                    if (float.TryParse(entry.Text, out lim))
                    {
                        budget.limit = lim;
                        entry.Text = budget.limit.ToString() + "р.";
                        t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                        await Task.WhenAll(t1);
                    }
                }
                else
                    entry.Text = budget.limit.ToString() + "р.";
            }
        }
        async void AddMoney(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parent = (Grid)(button.Parent);
            await parent.ScaleTo(0.85, 50);
            await parent.ScaleTo(1, 50);
            Entry entry = (Entry)button.CommandParameter;
            foreach (Object obj in parent.Children)
                if (TypeDescriptor.GetClassName(obj) == TypeDescriptor.GetClassName(button) && ((Button)obj).IsVisible == false)
                    button = (Button)obj;
            ServiceReference1.Target target = (ServiceReference1.Target)button.CommandParameter;
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            float money = 0;
            if (float.TryParse(entry.Text, out money))
                foreach (ServiceReference1.Target tar in budget.targets)
                {
                    if (tar.id == target.id)
                    {
                        tar.currentMoney += money;
                        tar.totalText = tar.currentMoney + "/" + tar.requiredMoney + "р.";
                    }
                }
            t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
            await Task.WhenAll(t1);

            await RefreshData();
        }
        void OnNameFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Target target = (ServiceReference1.Target)entry.ReturnCommandParameter;
            entry.Text = target.name;
        }
        async void OnNameUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ServiceReference1.Target target = (ServiceReference1.Target)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (target.name != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    ServiceReference1.Budget budget = null;
                    Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
                    await Task.WhenAll(t1);
                    foreach (ServiceReference1.Target tar in budget.targets)
                    {
                        if (tar.id == target.id)
                            tar.name = entry.Text;
                    }
                    t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                    await Task.WhenAll(t1);

                    await RefreshData();
                }
                else
                    entry.Text = target.name;
            }
        }
        async void OnDescriptionUnfocused(object sender, TextChangedEventArgs e)
        {
            Editor editor = (Editor)sender;
            Grid parent = (Grid)(editor.Parent);
            Button button = new Button();
            foreach (Object obj in parent.Children)
                if (TypeDescriptor.GetClassName(obj) == TypeDescriptor.GetClassName(button))
                    button = (Button)obj;
            ServiceReference1.Target target = (ServiceReference1.Target)button.CommandParameter;
            if (!editor.IsFocused)
            {
                if (target.name != editor.Text && editor.Text != null && editor.Text.Trim() != string.Empty)
                {
                    ServiceReference1.Budget budget = null;
                    Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
                    await Task.WhenAll(t1);
                    foreach (ServiceReference1.Target tar in budget.targets)
                        if (tar.id == target.id)
                        {
                            tar.description = editor.Text;
                            break;
                        }
                    t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                    await Task.WhenAll(t1);

                    await RefreshData();
                }
                else
                    editor.Text = target.description;
            }
        }
        async void CreateTarget(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            await PopupNavigation.Instance.PushAsync(new TargetPopup(this));
        }
        async void DeleteTarget(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.ScaleTo(0.9, 50);
            await button.ScaleTo(1, 50);
            bool result = await DisplayAlert("Удаление цели", "Вы действительно хотите удалить цель?", "Удалить", "Отмена");
            if (result)
            {
                ServiceReference1.Target target = (ServiceReference1.Target)button.CommandParameter;
                ServiceReference1.Budget budget = null;
                Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
                await Task.WhenAll(t1);
                ServiceReference1.Target[] tars = new ServiceReference1.Target[budget.targets.Length - 1];
                int i = 0;
                foreach (ServiceReference1.Target tar in budget.targets)
                    if (tar.id != target.id) {
                        tars[i] = tar;
                        i++;
                    }
                budget.targets = tars;
                t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                await Task.WhenAll(t1);
                await RefreshData();
            }
        }
        async void Return(object sender, EventArgs e)
        {
            await Arrow.ScaleTo(0.8, 50);
            await Arrow.ScaleTo(1, 50);
            await Navigation.PopAsync();
        }
    }
}