using CookieClicker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookieClicker.ViewModels
{
    class ItemPageViewModel 
    {
        private static List<Item> items;

        string miner = "Each Miner increase production by 3 for every second";
        string excavator = "Each Excavator increase production by 10 for every second";

        public List<Item> Items { get { return items; } }

        public ItemPageViewModel()
        {
            items = new List<Item>
            {
                new Item{ Name="Miner", ActionText="Buy", Description = miner, Rate=ItemRate.Miner, CostPerItem=ItemCost.Miner, Transactionable = true, Opacity = .5f },
                new Item{ Name="Excavator", ActionText="Buy", Description = excavator, Rate=ItemRate.Excavator, CostPerItem=ItemCost.Excavator, Transactionable = true, Opacity = .5f },
            };
        }
        public void UpdateAction(string _action, int _amount)
        {

            foreach (Item a in items)
            {
                a.ActionText = _action;
                a.CostCalculate(_action, _amount);
            }
        }
        public void CheckIfPurchasable()
        {
            foreach (Item a in items)
            {
                if(a.Cost > DataStorage.score)
                {
                    a.Transactionable = false;
                }
                else if (a.Cost <= DataStorage.score)
                {
                    a.Transactionable = true;
                }
            }
        }
        public void CheckIfSellable( int amount)
        {
            foreach (Item a in items)
            {
                if (a.Owned >= amount)
                {
                    // sellale in this case not purchasable
                    a.Transactionable = true;
                }
                else
                {
                    a.Transactionable = false;
                }
            }
        }
    }
}
