using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile_BasketballGameCreator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void startNewGame_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreGameSetup());
        }
        private async void VisitGithub_Click(object sender, EventArgs e)
        {
            String website = @"https://github.com/Baseballfan5303/VS-BasketballGameCreator";
            var uri = new Uri(website);

            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
