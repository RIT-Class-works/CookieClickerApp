using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatsPage : ContentPage
    {
        //private ImageSource src;
        public StatsPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Current.Text = "Current Point Earned: " + DataStorage.score;
                    Max.Text = "Highest Point Earned Record: " + DataStorage.max;
                    Click.Text = "Each Click: +1";
                    productivity.Text = "Passive Productivity: " + DataStorage.passiveProductivity + "/s";

                });
                return true; // runs again, or false to stop
            });
        }

        private async void ShareBtn_Clicked(object sender, EventArgs e)
        {
            ShareBtn.IsVisible = false;
            await CaptureScreenshotAndShare();
            ShareBtn.IsVisible = true;

        }
        async Task CaptureScreenshotAndShare()
        {
            var screenshot = await Screenshot.CaptureAsync();
            var stream = await screenshot.OpenReadAsync();

            var file = Path.Combine(FileSystem.CacheDirectory, "screenshot.png");

            byte[] bArray = new byte[stream.Length];
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (stream)
                {
                    stream.Read(bArray, 0, (int)stream.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }
    }
}