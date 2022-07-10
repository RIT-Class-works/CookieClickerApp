using System;
using System.Collections.Generic;
using System.Text;

namespace CookieClicker
{
    class DataStorage
    {
        public static string[] itemKeys = new string[] {
           "Miner",
           "Excavator",
        };
        public static string[] upgradeKeys = new string[] {
           "Certified Miner Licenses",
           "Legalized Excavator Licenses",
        };
        public static float score;
        public static float max;
        public static float passiveProductivity;
        public static float increment = 1.0f;
        public static Dictionary<string, int> itemOwned = new Dictionary<string, int>();
        public static Dictionary<string, bool> upgradeOwned = new Dictionary<string, bool>();

        public static void UpdateMaxScore()
        {
            if(score > max)
            {
                max = score;
            }
        }
    }
}
