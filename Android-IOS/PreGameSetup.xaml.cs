using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_BasketballGameCreator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreGameSetup : ContentPage
    {
        public static string awayTeamName;
        public static string homeTeamName;

        public static ArrayList awayTeamPlayers = new ArrayList();
        public static ArrayList homeTeamPlayers = new ArrayList();

        public static Entry tb1;

        public object important;

        public PreGameSetup()
        {
            InitializeComponent();
            tb1 = userTimeInput;
        }

        private async void nextScreen_Click(object sender, EventArgs e)
        {
            saveTeamInput();
            await Navigation.PushAsync(new BasketballGameScreen());
        }

        public void saveTeamInput()
        {
            awayTeamName = ATName.Text;
            homeTeamName = HTName.Text;

            awayTeamPlayers.Add(ATPG1.Text);
            awayTeamPlayers.Add(ATSG1.Text);
            awayTeamPlayers.Add(ATSF1.Text);
            awayTeamPlayers.Add(ATPF1.Text);
            awayTeamPlayers.Add(ATC1.Text);
            awayTeamPlayers.Add(AT6.Text);
            awayTeamPlayers.Add(AT7.Text);
            awayTeamPlayers.Add(AT8.Text);
            awayTeamPlayers.Add(AT9.Text);
            awayTeamPlayers.Add(AT10.Text);

            homeTeamPlayers.Add(HTPG1.Text);
            homeTeamPlayers.Add(HTSG1.Text);
            homeTeamPlayers.Add(HTSF1.Text);
            homeTeamPlayers.Add(HTPF1.Text);
            homeTeamPlayers.Add(HTC1.Text);
            homeTeamPlayers.Add(HT6.Text);
            homeTeamPlayers.Add(HT7.Text);
            homeTeamPlayers.Add(HT8.Text);
            homeTeamPlayers.Add(HT9.Text);
            homeTeamPlayers.Add(HT10.Text);
        }

        public static string getATName()
        {
            return awayTeamName;
        }
        public static string getHTName()
        {
            return homeTeamName;
        }
        public static ArrayList getATPlayers()
        {
            return awayTeamPlayers;
        }
        public static ArrayList getHTPlayers()
        {
            return homeTeamPlayers;
        }

        public static int getUserInputTime()
        {
            int a = Convert.ToInt32(tb1.Text);
            int b = a * 60;
            return b;
        }

    }
}