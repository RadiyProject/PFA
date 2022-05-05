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
            await AddMoneyToTargets();
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            Limit.Placeholder = budget.limit.ToString() + "р.";
            await Refresh();
        }
        async Task AddMoneyToTargets()
        {
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            budget.targets = budget.targetsN;
            if (budget.targets == null)
                budget.targets = new List<Target>();
            bool hasLimit = budget.hasLimit;
            DateTime today = DateTime.Today;
            foreach (Target tar in budget.targets)
            {
                if (hasLimit && tar.lastAccessTime != null)
                {
                    List<Cheque> cheques = Task.Run(() => App.Cheques.GetWithDateAsync(tar.lastAccessTime)).Result;
                    float sum = 0;
                    foreach (Cheque cheq in cheques)
                        sum += cheq.totalPrice;
                    float res = budget.limit - sum;
                    int days = (int)(today - tar.lastAccessTime).TotalDays;
                    if (days > 0)
                    {
                        if (res > 0)
                            tar.AddMoney(res / budget.targets.Count);
                        tar.AddMoney((days - 1) * budget.limit / budget.targets.Count);
                    }
                }
                tar.lastAccessTime = today;
            }
            budget.SetTargets();
            await App.Budget.Update(budget);
        }
        async void ActWithTarget(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            await button.ScaleTo(0.8, 50);
            await button.ScaleTo(1, 50);
            Target target = (Target)button.CommandParameter;
            if (target != null)
            {
                Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
                budget.targets = budget.targetsN;
                if (budget.targets == null)
                    budget.targets = new List<Target>();
                foreach (Target tar in budget.targets)
                {
                    if (tar.name == target.name && tar.deadLine == target.deadLine && tar.requiredMoney == target.requiredMoney
                        && tar.currentMoney == target.currentMoney)
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
                budget.SetTargets();
                await App.Budget.Update(budget);
            }
            if (button.RotationX == 0)
                await button.RotateXTo(180, 200);
            else
                await button.RotateXTo(0, 200);
            BindableLayout.SetItemsSource(BudgetStack, GetTargets());
        }
        List<Target> GetTargets()
        {
            return Task.Run(() => App.Budget.GetAsync()).Result.Last().targetsN;
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
            BindableLayout.SetItemsSource(BudgetStack, GetTargets());
        }
        private async Task ResetOpenedTargets()
        {
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            budget.targets = budget.targetsN;
            if (budget.targets == null)
                budget.targets = new List<Target>();
            foreach (Target tar in budget.targets)
            {
                tar.isOpened = false;
                tar.isClosed = true;
                tar.colFirst = 0.3f;
                tar.colSecond = 0.35f;
                tar.colThird = 0.28f;
                tar.colFourth = 0.07f;
            }
            budget.SetTargets();
            await App.Budget.Update(budget);
        }
        void OnLimitFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            entry.Text = budget.limit.ToString();
        }
        async void OnLimitUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            if (!entry.IsFocused)
            {
                if (budget.limit.ToString() != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    float lim = 0;
                    if (float.TryParse(entry.Text, out lim))
                    {
                        budget.limit = lim;
                        entry.Text = budget.limit.ToString() + "р.";
                        await App.Budget.Update(budget);
                    }
                }
                else
                    entry.Text = budget.limit.ToString() + "р.";
            }
        }
        void OnNameFocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Target target = (Target)entry.ReturnCommandParameter;
            entry.Text = target.name;
        }
        async void OnNameUnfocused(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            Target target = (Target)entry.ReturnCommandParameter;
            if (!entry.IsFocused)
            {
                if (target.name != entry.Text && entry.Text != null && entry.Text.Trim() != string.Empty)
                {
                    Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
                    budget.targets = budget.targetsN;
                    if (budget.targets == null)
                        budget.targets = new List<Target>();
                    foreach (Target tar in budget.targets)
                    {
                        if (tar.name == target.name && tar.deadLine == target.deadLine && tar.requiredMoney == target.requiredMoney
                            && tar.currentMoney == target.currentMoney)
                            tar.name = entry.Text;
                    }
                    budget.SetTargets();
                    await App.Budget.Update(budget);

                    BindableLayout.SetItemsSource(BudgetStack, GetTargets());
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
            Target target = (Target)button.CommandParameter;
            if (!editor.IsFocused)
            {
                if (target.name != editor.Text && editor.Text != null && editor.Text.Trim() != string.Empty)
                {
                    Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
                    budget.targets = budget.targetsN;
                    if (budget.targets == null)
                        budget.targets = new List<Target>();
                    foreach (Target tar in budget.targets)
                        if (tar.name == target.name && tar.deadLine == target.deadLine && tar.requiredMoney == target.requiredMoney
                            && tar.currentMoney == target.currentMoney)
                        {
                            tar.description = editor.Text;
                            break;
                        }
                    budget.SetTargets();
                    await App.Budget.Update(budget);

                    BindableLayout.SetItemsSource(BudgetStack, GetTargets());
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
                Target target = (Target)button.CommandParameter;
                Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
                budget.targets = budget.targetsN;
                if (budget.targets == null)
                    budget.targets = new List<Target>();
                foreach (Target tar in budget.targets)
                    if (tar.name == target.name && tar.deadLine == target.deadLine && tar.requiredMoney == target.requiredMoney
                        && tar.currentMoney == target.currentMoney)
                    {
                        budget.targets.Remove(tar);
                        break;
                    }
                budget.SetTargets();
                await App.Budget.Update(budget);
                BindableLayout.SetItemsSource(BudgetStack, GetTargets());
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