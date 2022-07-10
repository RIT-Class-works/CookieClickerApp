using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace CookieClicker
{
    public partial class App : Application
    {
        private string key_Score = "key_score";
        private string key_Max = "key_maxScore";
        public App()
        {
            
            InitializeComponent();
            DataStorage.score = float.Parse(Preferences.Get(key_Score, "0"));
            DataStorage.max = float.Parse(Preferences.Get(key_Max, "0"));

            for (int i = 0; i < DataStorage.itemKeys.Length; i++)
            {
                DataStorage.itemOwned.Add(DataStorage.itemKeys[i], int.Parse(Preferences.Get(DataStorage.itemKeys[i], "0")));
            }

            for (int i = 0; i < DataStorage.upgradeKeys.Length; i++)
            {
                DataStorage.upgradeOwned.Add(DataStorage.upgradeKeys[i], Preferences.Get(DataStorage.upgradeKeys[i], false));
            }

            MainPage = new Splash();
            
        }

        protected override void OnStart()
        {
            DataStorage.score = float.Parse(Preferences.Get(key_Score, "0"));
            DataStorage.max = float.Parse(Preferences.Get(key_Max, "0"));

            for (int i = 0; i < DataStorage.itemKeys.Length; i++)
            {
                if (DataStorage.itemOwned.ContainsKey(DataStorage.itemKeys[i]))
                {
                    DataStorage.itemOwned[DataStorage.itemKeys[i]] = int.Parse(Preferences.Get(DataStorage.itemKeys[i], "0"));
                }
            }

            for (int i = 0; i < DataStorage.upgradeKeys.Length; i++)
            {
                if (DataStorage.upgradeOwned.ContainsKey(DataStorage.upgradeKeys[i]))
                {
                    DataStorage.upgradeOwned[DataStorage.upgradeKeys[i]] = Preferences.Get(DataStorage.upgradeKeys[i], false);
                }
            }
        }

        protected override void OnSleep()
        {
            Preferences.Set(key_Score, string.Format("{0}", DataStorage.score));
            Preferences.Set(key_Max, string.Format("{0}", DataStorage.max));

            foreach (var entry in DataStorage.itemOwned)
            {
                Preferences.Set(entry.Key, string.Format("{0}", entry.Value));
            }
            foreach (var entry in DataStorage.upgradeOwned)
            {
                Preferences.Set(entry.Key, entry.Value);
            }
        }
         
        protected override void OnResume()
        {

        }
    }
}
