using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookieClicker.ViewModels;
using CookieClicker.Models;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpgradePage : ContentPage
    {
        private UpgradePageViewModel vm;
        public UpgradePage()
        {
            InitializeComponent();
            vm = (UpgradePageViewModel)BindingContext;

            foreach (Upgrade upgrade in vm.Upgrades)
            {
                if (DataStorage.upgradeOwned.ContainsKey(upgrade.Name) && DataStorage.upgradeOwned[upgrade.Name])
                {
                    upgrade.ActionText = "Purchased";
                    upgrade.Transactionable = false;
                    upgrade.Opacity = .5f;
                }
            }

            Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Score.Text = DataStorage.score + " Golds";
                    vm.CheckIfPurchasable();
                });
                return true; // runs again, or false to stop
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid grid = button.Parent as Grid;
            Label label = grid.Children.Where(c => Grid.GetRow(c) == 0 && Grid.GetColumn(c) == 1).Cast<Label>().First();
            switch (label.Text)
            {
                case "Certified Miner Licenses":
                    DataStorage.score -= vm.Upgrades[0].Cost;
                    DataStorage.upgradeOwned[label.Text] = true;
                    vm.Upgrades[0].ActionText = "Purchased";
                    vm.Upgrades[0].Transactionable = false;
                    break;
                case "Legalized Excavator Licenses":
                    DataStorage.score -= vm.Upgrades[1].Cost;
                    DataStorage.upgradeOwned[label.Text] = true;
                    vm.Upgrades[1].ActionText = "Purchased";
                    vm.Upgrades[1].Transactionable = false;
                    break;
                default:
                    //not handle
                    break;
            }
        }
    }
}