using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CookieClicker.Models;

namespace CookieClicker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            
            InitializeComponent();
            Score.Text = DataStorage.score + " Golds";
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    BoostBar.Progress -= .1;

                    if (Boost.IsVisible && BoostBar.Progress == 0)
                    {
                        Boost.IsVisible = false;
                        BoostBar.ProgressColor = Color.HotPink;
                        DataStorage.increment = DataStorage.increment / 2.0f;
                    }

                    float productivity = 0;
                    foreach (var entry in DataStorage.itemOwned)
                    {
                        if(entry.Key == "Miner")
                        {
                            int bonus = 1;
                            if (DataStorage.upgradeOwned[DataStorage.upgradeKeys[0]])
                            {
                                bonus = 2;
                            }
                            productivity += DataStorage.itemOwned[entry.Key] * (int)ItemRate.Miner * bonus;
                            DataStorage.score += DataStorage.itemOwned[entry.Key] * (int)ItemRate.Miner * bonus;
                        }
                        else if (entry.Key == "Excavator")
                        {
                            int bonus = 1;
                            if (DataStorage.upgradeOwned[DataStorage.upgradeKeys[1]])
                            {
                                bonus = 2;
                            }
                            productivity += DataStorage.itemOwned[entry.Key] * (int)ItemRate.Excavator * bonus;
                            DataStorage.score += DataStorage.itemOwned[entry.Key] * (int)ItemRate.Excavator * bonus;
                        }
                        
                    }
                    Score.Text = DataStorage.score + " Golds";
                    DataStorage.UpdateMaxScore();
                    DataStorage.passiveProductivity = productivity;
                });
                return true; // runs again, or false to stop
            });
        }
        
        private async void GoldMine_Clicked(object sender, EventArgs e)
        {
            if (!Boost.IsVisible)
            {
                BoostBar.Progress += .05;
            }
            
            if(BoostBar.Progress >= 1)
            {
                BoostBar.ProgressColor = Color.Red;
                Boost.IsVisible = true;
                DataStorage.increment = DataStorage.increment * 2.0f;
            }


            DataStorage.score += DataStorage.increment;
            Score.Text = DataStorage.score + " Golds";
            DataStorage.UpdateMaxScore();
            
            await GoldMine.ScaleTo(0.9, 150, Easing.SinIn);
            await GoldMine.ScaleTo(1, 150, Easing.SpringOut);
        }
    }
}
