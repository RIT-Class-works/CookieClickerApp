
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
    public partial class StorePage : ContentPage
    {
        private ItemPageViewModel vm;
        string currentAction = "Buy";
        private bool buyMode = true;
        public StorePage()
        {
            InitializeComponent();
            Score.Text = DataStorage.score + " Golds";
            Slider.Value = 0;
            vm = (ItemPageViewModel)BindingContext;

            foreach (Item item in vm.Items)
            {
                if (DataStorage.itemOwned.ContainsKey(item.Name))
                {
                    item.Owned = DataStorage.itemOwned[item.Name];
                }
            }

            Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UpdateSliderMax();
                });
                return true; // runs again, or false to stop
            });
        }


        private void Buy_Clicked(object sender, EventArgs e)
        {
            UpdateSliderMax();
            Slider.Value = 0;
            currentAction = "Buy";
            buyMode = true;
            vm.UpdateAction(currentAction, (int)Slider.Value);
            vm.CheckIfPurchasable();
        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            UpdateSliderMax();
            Slider.Value = 0;
            currentAction = "Sell";
            buyMode = false;
            vm.UpdateAction(currentAction, (int)Slider.Value);
            vm.CheckIfSellable((int)Slider.Value);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Amount.Text = "Amount: " + (int)Slider.Value;
            vm.UpdateAction(currentAction, (int)Slider.Value);
            if (buyMode)
            {
                vm.CheckIfPurchasable();
            }
            else
            {
                vm.CheckIfSellable((int)Slider.Value);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if((int)Slider.Value == 0)
            {
                return;
            }
            Button button = (Button)sender;
            if (button.Text == "Buy")
            {
                Grid grid = button.Parent as Grid;
                Label label = grid.Children.Where(c => Grid.GetRow(c) == 0 && Grid.GetColumn(c) == 2).Cast<Label>().First();

                switch (label.Text)
                {
                    case "Miner":
                        if (DataStorage.itemOwned.ContainsKey("Miner"))
                        {

                            DataStorage.score -= vm.Items[0].Cost;
                            DataStorage.itemOwned["Miner"] = DataStorage.itemOwned["Miner"] + (int)Slider.Value;
                            vm.Items[0].Owned = DataStorage.itemOwned["Miner"];

                            Slider.Value = 0;
                        }
                        else
                        {
                            DataStorage.score -= vm.Items[0].Cost;
                            DataStorage.itemOwned.Add("Miner", (int)Slider.Value);
                            vm.Items[0].Owned = DataStorage.itemOwned["Miner"];

                            Slider.Value = 0;
                        }
                        break;
                    case "Excavator":
                        if (DataStorage.itemOwned.ContainsKey("Excavator"))
                        {
                            DataStorage.score -= vm.Items[1].Cost;
                            DataStorage.itemOwned["Excavator"] = DataStorage.itemOwned["Excavator"] + (int)Slider.Value;
                            vm.Items[1].Owned = DataStorage.itemOwned["Excavator"];

                            Slider.Value = 0;
                        }
                        else
                        {
                            DataStorage.score -= vm.Items[1].Cost;
                            DataStorage.itemOwned.Add("Excavator", (int)Slider.Value);
                            vm.Items[1].Owned = DataStorage.itemOwned["Excavator"];

                            Slider.Value = 0;
                        }
                        break;
                    default:
                        //not handle
                        break;
                }
            }
            else if (button.Text == "Sell")
            {
                Grid grid = button.Parent as Grid;
                Label label = grid.Children.Where(c => Grid.GetRow(c) == 0 && Grid.GetColumn(c) == 2).Cast<Label>().First();

                switch (label.Text)
                {
                    case "Miner":

                        DataStorage.score += vm.Items[0].Cost;
                        DataStorage.itemOwned["Miner"] = DataStorage.itemOwned["Miner"] - (int)Slider.Value;
                        vm.Items[0].Owned = DataStorage.itemOwned["Miner"];

                        Slider.Value = 0;

                        break;
                    case "Excavator":

                        DataStorage.score += vm.Items[1].Cost;
                        DataStorage.itemOwned["Excavator"] = DataStorage.itemOwned["Excavator"] - (int)Slider.Value;
                        vm.Items[1].Owned = DataStorage.itemOwned["Excavator"];

                        Slider.Value = 0;

                        break;
                    default:
                        //not handle
                        break;
                }
            }
        }

        private void UpdateSliderMax()
        {
            Score.Text = DataStorage.score + " Golds";
            if (buyMode)
            {
                int max = vm.Items[0].MaxAmountPurchasable();

                if (max < 1)
                {
                    Slider.Maximum = 1;
                    return;
                }
                else
                {
                    Slider.Maximum = max;
                }
            }
            else
            {
                if (DataStorage.itemOwned.ContainsKey("Miner") && DataStorage.itemOwned["Miner"] > 0)
                {
                    Slider.Maximum = DataStorage.itemOwned["Miner"];
                }
                else if (DataStorage.itemOwned.ContainsKey("Excavator") && DataStorage.itemOwned["Excavator"] > 0)
                {
                    Slider.Maximum = DataStorage.itemOwned["Excavator"];
                }
                else
                {
                    Slider.Maximum = 1;
                }

            }
        }
    }
}