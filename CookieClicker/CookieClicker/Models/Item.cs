using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookieClicker.Models
{
    public enum ItemRate
    {
        Miner = 3,
        Excavator = 10,
    }
    public enum ItemCost
    {
        Miner = 30,
        Excavator = 100,
    }
    public class Item : INotifyPropertyChanged
    {

        private string actionText;
        private bool transactionble;
        private int cost;
        private int owned;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string ActionText {
            get
            {
                return actionText;
            }
            set
            {
                actionText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActionText"));
            }
        }
        public string Description { get; set; }
        public ItemRate Rate { get; set; }
        public ItemCost CostPerItem { get; set; }
        public int Cost { get { return cost; } }
        public bool Transactionable
        {
            get
            {
                return transactionble;
            }
            set
            {
                transactionble = value;
                if (transactionble)
                {
                    Opacity = 1.0f;
                }
                else
                {
                    Opacity = .5f;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Transactionable"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Opacity"));
            }
        }
        public float Opacity { get; set; }
        public int Owned
        {
            get
            {
                return owned;
            }
            set
            {
                owned = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Owned"));
            }
        }

        public void CostCalculate( string _action, int _amount)
        {
            if(_action == "Buy")
            {
                float rate = (.5f * (owned + _amount)) + 1;
                cost = (int)(_amount * rate * (int)CostPerItem);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
            else if (_action == "Sell")
            {
                cost = _amount * (int)((int)CostPerItem*.8f);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
        }

        public int MaxAmountPurchasable()
        {
            bool found = false;
            int amount = 0;
            while (!found)
            {
                float rate = (.5f * (owned + amount)) + 1;
                float maxCost = (int)(amount * rate * (int)CostPerItem);
                if (maxCost > DataStorage.score)
                {
                    found = true;
                }
                else
                {
                    amount++;
                }
            }

            return amount - 1;

        }

    }
}
