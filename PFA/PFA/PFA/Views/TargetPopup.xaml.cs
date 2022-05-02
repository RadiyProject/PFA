using PFA.Database;
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
    public partial class TargetPopup
    {
        Budget page;
        public TargetPopup(Budget page)
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
        async void newTarget(object sender, EventArgs e)
        {
            Grid parent = (Grid)((Label)sender).Parent;
            await parent.ScaleTo(0.9, 50);
            await parent.ScaleTo(1, 50);
            int daysCount = 0;
            float money = 0;
            if (popup_Name.Text == null)
                popup_Name.Text = "Новая цель";
            if (popup_Amount.Text == null || !float.TryParse(popup_Amount.Text, out money)
                || money <= 0)
                money = 25000;
            if (popup_DaysCount.Text == null || !int.TryParse(popup_DaysCount.Text, out daysCount)
                || daysCount <= 0)
                daysCount = 45;
            DateTime today = DateTime.Today;
            Target target = new Target(popup_Name.Text, today.AddDays(daysCount));
            target.requiredMoney = money;
            target.description = "...";
            target.lastAccessTime = DateTime.Today;
            target.SetTotal();
            Database.Budget budget = Task.Run(() => App.Budget.GetAsync()).Result.Last();
            budget.targets = budget.targetsN;
            if (budget.targets == null)
                budget.targets = new List<Target>();
            budget.targets.Add(target);
            budget.SetTargets();
            await App.Budget.Update(budget);
            await page.Refresh();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}