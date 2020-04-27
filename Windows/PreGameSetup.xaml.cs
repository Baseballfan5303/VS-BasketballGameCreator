using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BasketballGameCreator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PreGameSetup : Page
    {
        public static string awayTeamName;
        public static string homeTeamName;

        public static ArrayList awayTeamPlayers = new ArrayList();
        public static ArrayList homeTeamPlayers = new ArrayList();

        public static TextBox tb1;

        public object important;

        public PreGameSetup()
        {
            this.InitializeComponent();
            tb1 = userTimeInput;
        }

        private void nextScreen_Click(object sender, RoutedEventArgs e)
        {
            saveTeamInput();
            if(important.Equals(0))
                this.Frame.Navigate(typeof(BasketballGameScreen2));
            else
                this.Frame.Navigate(typeof(BasketballGameScreen));
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
            awayTeamPlayers.Add(AT11.Text);
            awayTeamPlayers.Add(AT12.Text);

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
            homeTeamPlayers.Add(HT11.Text);
            homeTeamPlayers.Add(HT12.Text);
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

        private void gameTypeCBSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = gameTypeCBSC.SelectedItem.ToString();
            switch (item)
            {
                case "No Free Throws":
                    important = 0;
                    gameTypeChosen.Text = "Free throws will not occur.";
                    break;
                case "Free Throws":
                    important = 1;
                    gameTypeChosen.Text = "Free throws will occur.";
                    break;
            }
        }
    }
}
