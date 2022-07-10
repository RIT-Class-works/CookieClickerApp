using CookieClicker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookieClicker.ViewModels
{
    class UpgradePageViewModel
    {
        private static List<Upgrade> upgrades;
        private string miner = "Each Miner productivity permanently double";
        private string excavator = "Each Excavator productivity permanently double";
        public List<Upgrade> Upgrades { get { return upgrades; } }

        public UpgradePageViewModel()
        {
            upgrades = new List<Upgrade>
            {
                new Upgrade()
                {
                    Name = "Certified Miner Licenses",
                    ActionText = "Buy",
                    Description = miner,
                    Transactionable = true,
                    Cost = (int)UpgradeCost.Miner
                },
                new Upgrade()
                {
                    Name = "Legalized Excavator Licenses",
                    ActionText = "Buy",
                    Description = excavator,
                    Transactionable = true,
                    Cost = (int)UpgradeCost.Excavator
                }
            };
        }
        public void CheckIfPurchasable()
        {
            foreach (Upgrade a in upgrades)
            {
                if (a.Cost > DataStorage.score)
                {
                    a.Transactionable = false;
                }
                else if (a.Cost <= DataStorage.score && !DataStorage.upgradeOwned[a.Name])
                {
                    a.Transactionable = true;
                }
            }
        }
    }
}
