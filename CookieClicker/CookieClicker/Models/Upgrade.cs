using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookieClicker.Models
{

    public enum UpgradeCost
    {
        Miner = 30000,
        Excavator = 100000,
    }
    class Upgrade : INotifyPropertyChanged
    {
        private string actionText;
        private bool transactionble;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string ActionText
        {
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
        public int Cost { get; set; }
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
    }
}
