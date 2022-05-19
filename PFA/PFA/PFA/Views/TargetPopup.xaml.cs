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
            if (PopupNavigation.Instance.PopupStack.Count > 0)
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
            ServiceReference1.Budget budget = null;
            Task t1 = Task.Run(() => budget = App.server.GetBudget((string)App.Current.Properties["user"]));
            await Task.WhenAll(t1);
            if (budget != null)
            {
                ServiceReference1.Target[] targets = new ServiceReference1.Target[budget.targets.Length];
                if (targets == null || targets.Length == 0)
                {
                    targets = new ServiceReference1.Target[1];
                    targets[0] = new ServiceReference1.Target();
                    targets[0].budgetId = budget.id;
                    targets[0].name = popup_Name.Text;
                    targets[0].requiredMoney = money;
                    targets[0].currentMoney = 0;
                    targets[0].totalText = targets[0].currentMoney + "/" + targets[0].requiredMoney + "р.";
                    targets[0].deadLine = today.AddDays(daysCount);
                    targets[0].deadLineText = targets[0].deadLine.ToString("d");
                    targets[0].description = "...";
                    targets[0].lastAccessTime = DateTime.Today;
                }
                else
                {
                    targets = new ServiceReference1.Target[budget.targets.Length + 1];
                    int i = 0;
                    foreach (ServiceReference1.Target tar in budget.targets) {
                        targets[i] = tar;
                        i++;
                    }
                    targets[i] = new ServiceReference1.Target();
                    targets[i].budgetId = budget.id;
                    targets[i].name = popup_Name.Text;
                    targets[i].requiredMoney = money;
                    targets[i].currentMoney = 0;
                    targets[i].totalText = targets[i].currentMoney + "/" + targets[i].requiredMoney + "р.";
                    targets[i].deadLine = today.AddDays(daysCount);
                    targets[i].deadLineText = targets[i].deadLine.ToString("d");
                    targets[i].description = "...";
                    targets[i].lastAccessTime = DateTime.Today;

                }
                budget.targets = targets;
                t1 = Task.Run(() => budget = App.server.UpdateBudget(budget));
                await Task.WhenAll(t1);
                await page.Refresh();
            }
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }
    }
}