using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookieClicker
{
    public class Splash : ContentPage
    {
        Image splashImage;
        public Splash()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var container = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "splash.png"
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            container.Children.Add(splashImage);
            this.Content = container;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1, 1000);
            await splashImage.ScaleTo(0.8, 1000, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new GameTab());
        }
    }
}
