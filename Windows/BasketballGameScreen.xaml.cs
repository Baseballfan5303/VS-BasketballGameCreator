using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BasketballGameCreator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasketballGameScreen : Page
    {
        public static ArrayList awayTeamPlayersX = new ArrayList();
        public static ArrayList homeTeamPlayersX = new ArrayList();
        public static ArrayList awayOnFloor = new ArrayList();
        public static ArrayList homeOnFloor = new ArrayList();

        public static int ballFirst, ATPlayer, HTPlayer, jumpShot, layUp, postShot, shotLoc, atScore, htScore, whichShot, scoreIncrement, important, ftSender, numFT;
        public static int xyz;

        public static Ellipse tle, tre, mle, mre, llb, lrb, lbl, rbl, lct, rct, mlt, mrt, tlt, trt, tmt, ftl;

        public static object teamWBall, FTSenderXYZ; //teamWBall: away = 0, home = 1 ... ^^whichShot: j = 0,4 , pm = 2, l = 1,3

        public static Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        public static Windows.Storage.StorageFile gameLog;

        public static string FINALGAMELOG;
        public static string ATNameXYZ, HTNameXYZ;

        public BasketballGameScreen()
        {
            this.InitializeComponent();

            gameStart();
        }

        public async void gameStart()
        {
            FINALGAMELOG = "test";
            gameLog = await storageFolder.CreateFileAsync("GameLog.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            awayTeamPlayersX = PreGameSetup.getATPlayers(); homeTeamPlayersX = PreGameSetup.getHTPlayers();
            awayOnFloor.Add(awayTeamPlayersX[0]); awayOnFloor.Add(awayTeamPlayersX[1]); awayOnFloor.Add(awayTeamPlayersX[2]); awayOnFloor.Add(awayTeamPlayersX[3]); awayOnFloor.Add(awayTeamPlayersX[4]);
            homeOnFloor.Add(homeTeamPlayersX[0]); homeOnFloor.Add(homeTeamPlayersX[1]); homeOnFloor.Add(homeTeamPlayersX[2]); homeOnFloor.Add(homeTeamPlayersX[3]); homeOnFloor.Add(homeTeamPlayersX[4]);

            AOFP1.Text = awayOnFloor[0].ToString(); AOFP2.Text = awayOnFloor[1].ToString(); AOFP3.Text = awayOnFloor[2].ToString(); AOFP4.Text = awayOnFloor[3].ToString(); AOFP5.Text = awayOnFloor[4].ToString();
            HOFP1.Text = homeOnFloor[0].ToString(); HOFP2.Text = homeOnFloor[1].ToString(); HOFP3.Text = homeOnFloor[2].ToString(); HOFP4.Text = homeOnFloor[3].ToString(); HOFP5.Text = homeOnFloor[4].ToString();

            ATNameT.Text = PreGameSetup.getATName().Substring(0, 3);
            HTNameT.Text = PreGameSetup.getHTName().Substring(0, 3);
            ATNameXYZ = PreGameSetup.getATName().Substring(0, 3);
            HTNameXYZ = PreGameSetup.getHTName().Substring(0, 3);
            ATNameW.Text = PreGameSetup.getATName().Substring(0, 3);
            HTNameW.Text = PreGameSetup.getHTName().Substring(0, 3);

            awayStartersA.Content = PreGameSetup.getATName().Substring(0, 3);
            homeStartersA.Content = PreGameSetup.getHTName().Substring(0, 3);
            awayBenchA.Content = PreGameSetup.getATName().Substring(0, 3);
            homeBenchA.Content = PreGameSetup.getHTName().Substring(0, 3);

            currentQuarter.Text = "Q1"; xyz = 0;

            atScore = 0; htScore = 0; scoreIncrement = 0; teamWBall = whoGetsBallFirst(); important = 0; ftSender = 0; numFT = 0;

            tle = topLeftElbow; tre = topRightElbow; mle = midLeftElbow; mre = midRightElbow; llb = lowLeftBlock; lrb = lowRightBlock; lbl = leftBaseline; rbl = rightBaseLine; 
            lct = leftCornerThree; rct = rightCornerThree; mlt = midLeftThree; mrt = midRightThree; tlt = topLeftThree; trt = topRightThree; tmt = topMidThree; ftl = charityStripe;
        }

        private async void gameOver_Click(object sender, RoutedEventArgs e)
        {
            await Windows.Storage.FileIO.AppendTextAsync(gameLog, "The game is over." + "\n");
            await Windows.Storage.FileIO.AppendTextAsync(gameLog, "The quarters were " + (PreGameSetup.getUserInputTime() / 60) + " minutes long." + "\n");
            await Windows.Storage.FileIO.AppendTextAsync(gameLog, ATNameT.Text + "- Score: " + atScore.ToString() + "." + "\n");
            await Windows.Storage.FileIO.AppendTextAsync(gameLog, HTNameT.Text + "- Score: " + htScore.ToString() + "." + "\n");
            FINALGAMELOG = await Windows.Storage.FileIO.ReadTextAsync(gameLog);
            this.Frame.Navigate(typeof(GameLogScreen));
        }

        public static string getFINALGAMELOG()
        { return FINALGAMELOG; }
        public static string getAwayTeamNameForGL()
        { return ATNameXYZ; }
        public static string getHomeTeamNameForGL()
        { return HTNameXYZ; }

        public async void addActionBarTextToGameLog()
        {
            await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar.Text + "\n");
        }

        public static void clearCircleColors()
        {
            tle.Fill = new SolidColorBrush(Colors.Black);
            tre.Fill = new SolidColorBrush(Colors.Black);
            mle.Fill = new SolidColorBrush(Colors.Black);
            mre.Fill = new SolidColorBrush(Colors.Black);
            llb.Fill = new SolidColorBrush(Colors.Black);
            lrb.Fill = new SolidColorBrush(Colors.Black);
            lbl.Fill = new SolidColorBrush(Colors.Black);
            rbl.Fill = new SolidColorBrush(Colors.Black);
            lct.Fill = new SolidColorBrush(Colors.Black);
            rct.Fill = new SolidColorBrush(Colors.Black);
            mlt.Fill = new SolidColorBrush(Colors.Black);
            mrt.Fill = new SolidColorBrush(Colors.Black);
            tlt.Fill = new SolidColorBrush(Colors.Black);
            trt.Fill = new SolidColorBrush(Colors.Black);
            tmt.Fill = new SolidColorBrush(Colors.Black);
            ftl.Fill = new SolidColorBrush(Colors.Black);
        }

        public static int whoGetsBallFirst()
        { Random r = new Random(); ballFirst = r.Next(0, 2); return ballFirst; }
        public static int whichAwayPlayerShoots()
        { Random r = new Random(); ATPlayer = r.Next(0, 5); return ATPlayer; }
        public static int whichHomePlayerShoots()
        { Random r = new Random(); HTPlayer = r.Next(0, 5); return HTPlayer; }
        public static int shotScenarios()
        { Random r = new Random(); important = r.Next(0, 10); return important; }

        public static int getWAPS()
        { return ATPlayer; }
        public static int getWHPS()
        { return HTPlayer;  }
        
        public static string ChosenShot()
        {
            Random w = new Random(); whichShot = w.Next(0, 5);
            if(whichShot == 0 || whichShot == 4)
            {
                Random r = new Random(); jumpShot = r.Next(0, 50);
                if (jumpShot < 25)
                { return "normal jumper"; }
                else if (jumpShot > 24 && jumpShot < 34)
                { return "stepback jumper"; }
                else if (jumpShot > 33 && jumpShot < 39)
                { return "off-balance jumper"; }
                else
                { return "jumper of any type"; }
            }
            else if(whichShot == 2)
            {
                Random r = new Random(); postShot = r.Next(0, 41);
                whichShot = 1;
                if (postShot < 20)
                { return "post fade shot"; }
                else if (postShot > 19 && postShot < 30)
                { return "post hook shot"; }
                else if (postShot > 29 && postShot < 33)
                { return "post hop-stepback shot"; }
                else
                { return "post move shot of any type"; }
            }
            else
            {
                Random r = new Random(); layUp = r.Next(0, 41);
                whichShot = 2;
                if (layUp < 20)
                { return "normal layup"; }
                else if (layUp > 19 && layUp < 26)
                { return "floater/runner"; }
                else if (layUp > 25 && layUp < 28)
                { return "eurostep layup"; }
                else if (layUp > 27 && layUp < 35)
                { return "spin layup"; }
                else if (layUp > 34 && layUp < 44)
                { return "reverse layup"; }
                else
                { return "layup of any type"; }
            }
        }

        public static string whatLoc()
        {
            clearCircleColors();
            if(whichShot == 0 || whichShot == 4)
            {
                Random a1 = new Random(); shotLoc = a1.Next(0, 140);
                if(shotLoc < 5)
                { mle.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid left elbow."; }
                else if(shotLoc > 4 && shotLoc < 10)
                { tle.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the high left elbow."; }
                else if (shotLoc > 9 && shotLoc < 15)
                { mre.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid right elbow."; }
                else if (shotLoc > 14 && shotLoc < 20)
                { tre.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the high right elbow."; }
                else if (shotLoc > 19 && shotLoc < 25)
                { lbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the left baseline."; }
                else if (shotLoc > 24 && shotLoc < 30)
                { rbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the right baseline."; }
                else if (shotLoc > 29 && shotLoc < 45)
                { lct.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the left corner three."; }
                else if (shotLoc > 44 && shotLoc < 60)
                { rct.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the right corner three."; }
                else if (shotLoc > 59 && shotLoc < 75)
                { mlt.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the left mid three."; }
                else if (shotLoc > 74 && shotLoc < 90)
                { mrt.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the right mid three."; }
                else if (shotLoc > 89 && shotLoc < 105)
                { tlt.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the left top three."; }
                else if (shotLoc > 104 && shotLoc < 120)
                { trt.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the right top three."; }
                else if (shotLoc > 119 && shotLoc < 135)
                { tmt.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 3; return "from the top three."; }
                else
                { ftl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the free throw line."; }
            }
            else if(whichShot == 2)
            {
                Random a2 = new Random(); shotLoc = a2.Next(0, 75);
                if (shotLoc < 15)
                { llb.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the left block."; }
                else if (shotLoc > 14 && shotLoc < 20)
                { mle.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid left elbow."; }
                else if (shotLoc > 19 && shotLoc < 25)
                { tle.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the high left elbow."; }
                else if (shotLoc > 24 && shotLoc < 40)
                { lrb.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the right block."; }
                else if (shotLoc > 39 && shotLoc < 45)
                { mre.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid right elbow."; }
                else if (shotLoc > 44 && shotLoc < 50)
                { tre.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the high right elbow."; }
                else if (shotLoc > 49 && shotLoc < 60)
                { lbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the left baseline."; }
                else if(shotLoc > 59 && shotLoc < 70)
                { rbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the right baseline."; }
                else
                { ftl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the free throw line."; }
            }
            else
            {
                Random a3 = new Random(); shotLoc = a3.Next(0, 70);
                if(shotLoc < 15)
                { llb.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the left block."; }
                else if(shotLoc > 14 && shotLoc < 30)
                { lrb.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the right block."; }
                else if (shotLoc > 29 && shotLoc < 40)
                { mle.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid left elbow."; }
                else if (shotLoc > 39 && shotLoc < 50)
                { mre.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the mid right elbow."; }
                else if (shotLoc > 49 && shotLoc < 60)
                { lbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the left baseline."; }
                else
                { rbl.Fill = new SolidColorBrush(Colors.Green); scoreIncrement = 2; return "from the right baseline."; }
            }
        }

        private async void StartQuarter_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(startQ1))
            {
                //await Windows.Storage.FileIO.AppendTextAsync(gameLog, "Q1 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n");
                TheAlgorithm();
                startQ1.Visibility = Visibility.Collapsed;
                xyz = PreGameSetup.getUserInputTime();
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += dtTicker;
                dt.Start();
            }
            else if (sender.Equals(startQ2))
            {
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, "Q1 ends, Q2 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n");
                TheAlgorithm();
                startQ2.Visibility = Visibility.Collapsed;
                currentQuarter.Text = "Q2";
                xyz = PreGameSetup.getUserInputTime();
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick -= dtTicker;
                dt.Start();
            }
            else if (sender.Equals(startQ3))
            {
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, "Q2 ends, Q3 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n");
                TheAlgorithm();
                startQ3.Visibility = Visibility.Collapsed;
                currentQuarter.Text = "Q3";
                xyz = PreGameSetup.getUserInputTime();
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick -= dtTicker;
                dt.Start();
            }
            else
            {
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, "Q3 ends, Q4 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n");
                TheAlgorithm();
                startQ4.Visibility = Visibility.Collapsed;
                currentQuarter.Text = "Q4";
                xyz = PreGameSetup.getUserInputTime();
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick -= dtTicker;
                dt.Start();
            }
        }

        private void dtTicker(object sender, object e)
        {
            if(xyz != 0)
            {
                xyz--;
                int min = (int)(xyz / 60);
                int sec = (int)(xyz % 60);
                if(sec < 10)
                    uiTimer.Text = "0" + min.ToString() + ":" + "0" + sec.ToString();
                else
                    uiTimer.Text = "0" + min.ToString() + ":" + sec.ToString();
            }
            else
            {
                uiTimer.Text = "Done.";
            }
        }

        private async void ChangeAwayFloor_Click(object sender, RoutedEventArgs e)
        {
            if(sender.Equals(awayStartersA))
            {
                AOFP1.Text = awayTeamPlayersX[5].ToString(); AOFP2.Text = awayTeamPlayersX[6].ToString(); AOFP3.Text = awayTeamPlayersX[7].ToString(); AOFP4.Text = awayTeamPlayersX[8].ToString(); AOFP5.Text = awayTeamPlayersX[9].ToString();
                awayOnFloor[0] = awayTeamPlayersX[5].ToString(); awayOnFloor[1] = awayTeamPlayersX[6].ToString(); awayOnFloor[2] = awayTeamPlayersX[7].ToString(); awayOnFloor[3] = awayTeamPlayersX[8].ToString(); awayOnFloor[4] = awayTeamPlayersX[9].ToString();
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, ATNameT.Text + "'s bench was brought in." + "\n");
                TheAlgorithm();
            }
            else if(sender.Equals(awayBenchA))
            {
                AOFP1.Text = awayTeamPlayersX[0].ToString(); AOFP2.Text = awayTeamPlayersX[1].ToString(); AOFP3.Text = awayTeamPlayersX[2].ToString(); AOFP4.Text = awayTeamPlayersX[3].ToString(); AOFP5.Text = awayTeamPlayersX[4].ToString();
                awayOnFloor[0] = awayTeamPlayersX[0].ToString(); awayOnFloor[1] = awayTeamPlayersX[1].ToString(); awayOnFloor[2] = awayTeamPlayersX[2].ToString(); awayOnFloor[3] = awayTeamPlayersX[3].ToString(); awayOnFloor[4] = awayTeamPlayersX[4].ToString();
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, ATNameT.Text + "'s starters were brought in." + "\n");
                TheAlgorithm();
            }
        }
        private async void ChangeHomeFloor_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(homeStartersA))
            {
                HOFP1.Text = homeTeamPlayersX[5].ToString(); HOFP2.Text = homeTeamPlayersX[6].ToString(); HOFP3.Text = homeTeamPlayersX[7].ToString(); HOFP4.Text = homeTeamPlayersX[8].ToString(); HOFP5.Text = homeTeamPlayersX[9].ToString();
                homeOnFloor[0] = homeTeamPlayersX[5].ToString(); homeOnFloor[1] = homeTeamPlayersX[6].ToString(); homeOnFloor[2] = homeTeamPlayersX[7].ToString(); homeOnFloor[3] = homeTeamPlayersX[8].ToString(); homeOnFloor[4] = homeTeamPlayersX[9].ToString();
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, HTNameT.Text + "'s bench was brought in." + "\n");
                TheAlgorithm();
            }
            else if (sender.Equals(homeBenchA))
            {
                HOFP1.Text = homeTeamPlayersX[0].ToString(); HOFP2.Text = homeTeamPlayersX[1].ToString(); HOFP3.Text = homeTeamPlayersX[2].ToString(); HOFP4.Text = homeTeamPlayersX[3].ToString(); HOFP5.Text = homeTeamPlayersX[4].ToString();
                homeOnFloor[0] = homeTeamPlayersX[0].ToString(); homeOnFloor[1] = homeTeamPlayersX[1].ToString(); homeOnFloor[2] = homeTeamPlayersX[2].ToString(); homeOnFloor[3] = homeTeamPlayersX[3].ToString(); homeOnFloor[4] = homeTeamPlayersX[4].ToString();
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, HTNameT.Text + "'s starters were brought in." + "\n");
                TheAlgorithm();
            }
        }

        private async void MakeButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBar2.Visibility = Visibility.Collapsed;
            shotScenarios();
            if (teamWBall.Equals(0) && important != 4 && important != 5)
            {
                atScore += scoreIncrement;
                ATScoreT.Text = atScore.ToString();
                teamWBall = 1;
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, awayOnFloor[getWAPS()].ToString() + " made the shot." + "\n");
                TheAlgorithm();
            }
            else if(teamWBall.Equals(1) && important != 4 && important != 5)
            {
                htScore += scoreIncrement;
                HTScoreT.Text = htScore.ToString();
                teamWBall = 0;
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, homeOnFloor[getWHPS()].ToString() + " made the shot." + "\n");
                TheAlgorithm();
            }
            else
            {
                if (teamWBall.Equals(0))
                {
                    atScore += scoreIncrement;
                    ATScoreT.Text = atScore.ToString();
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, awayOnFloor[getWAPS()].ToString() + " made the shot." + "\n");
                }
                else if (teamWBall.Equals(1))
                {
                    htScore += scoreIncrement;
                    HTScoreT.Text = htScore.ToString();
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, homeOnFloor[getWHPS()].ToString() + " made the shot." + "\n");
                }
                somethingHappenedOnMake();
            }
        }

        private async void MissButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBar2.Visibility = Visibility.Collapsed;
            shotScenarios();
            if (teamWBall.Equals(0) && important != 0 && important != 9 && important != 4 && important != 5)
            {
                teamWBall = 1;
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, awayOnFloor[getWAPS()].ToString() + " missed the shot." + "\n");
                TheAlgorithm();
            }
            else if (teamWBall.Equals(1) && important != 0 && important != 9 && important != 4 && important != 5)
            {
                teamWBall = 0;
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, homeOnFloor[getWHPS()].ToString() + " missed the shot." + "\n");
                TheAlgorithm();
            }
            else
            {
                somethingHappenedOnMiss();
            }
        }

        private async void MakeFTButton_Click(object sender, RoutedEventArgs e)
        {
            if(FTSenderXYZ.Equals(0))
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.Visibility = Visibility.Collapsed;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    atScore++;
                    ATScoreT.Text = atScore.ToString();
                    teamWBall = 1;
                }
                else
                {
                    ActionBar2.Visibility = Visibility.Collapsed;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    htScore++;
                    HTScoreT.Text = htScore.ToString();
                    teamWBall = 0;
                }
                TheAlgorithm();
            }
            else
            {
                if(numFT > 1)
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.Visibility = Visibility.Visible;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        atScore++;
                        ATScoreT.Text = atScore.ToString();
                    }
                    else
                    {
                        ActionBar2.Visibility = Visibility.Visible;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        htScore++;
                        HTScoreT.Text = htScore.ToString();
                    }
                    numFT--;
                }
                else
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.Visibility = Visibility.Collapsed;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        atScore++;
                        ATScoreT.Text = atScore.ToString();
                        teamWBall = 1;
                    }
                    else
                    {
                        ActionBar2.Visibility = Visibility.Collapsed;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        htScore++;
                        HTScoreT.Text = htScore.ToString();
                        teamWBall = 0;
                    }
                    TheAlgorithm();
                }
            }
        }

        private async void MissFTButton_Click(object sender, RoutedEventArgs e)
        {
            if(FTSenderXYZ.Equals(0))
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.Visibility = Visibility.Collapsed;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 1;
                }
                else
                {
                    ActionBar2.Visibility = Visibility.Collapsed;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 0;
                }
                TheAlgorithm();
            }
            else
            {
                if (numFT > 1)
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.Visibility = Visibility.Visible;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    }
                    else
                    {
                        ActionBar2.Visibility = Visibility.Visible;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    }
                    numFT--;
                }
                else
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.Visibility = Visibility.Collapsed;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        teamWBall = 1;
                    }
                    else
                    {
                        ActionBar2.Visibility = Visibility.Collapsed;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                        await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                        teamWBall = 0;
                    }
                    TheAlgorithm();
                }
            }
        }

        public async void somethingHappenedOnMiss()
        {
            if (important == 0 || important == 9)
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.Visibility = Visibility.Visible;
                    ActionBar2.Text = awayOnFloor[whichAwayPlayerShoots()].ToString() + " got the offensive rebound. ";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 0;
                    TheAlgorithm();
                }
                else
                {
                    ActionBar2.Visibility = Visibility.Visible;
                    ActionBar2.Text = homeOnFloor[whichHomePlayerShoots()].ToString() + " got the offensive rebound. ";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 1;
                    TheAlgorithm();
                }
            }
            else if(important == 4 || important == 5)
            {
                numFT = scoreIncrement;
                FTSenderXYZ = 1;
                if (teamWBall.Equals(0))
                {
                    ActionBar2.Visibility = Visibility.Visible;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " gets " + scoreIncrement + " free throws.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 0;
                }
                else
                {
                    ActionBar2.Visibility = Visibility.Visible;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " gets " + scoreIncrement + " free throws.";
                    await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                    teamWBall = 1;
                }
            }
        }

        public async void somethingHappenedOnMake()
        {
            numFT = 1;
            FTSenderXYZ = 0;
            if (teamWBall.Equals(0))
            {
                ActionBar2.Visibility = Visibility.Visible;
                ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " gets 1 free throw.";
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                teamWBall = 0;
            }
            else
            {
                ActionBar2.Visibility = Visibility.Visible;
                ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " gets 1 free throw.";
                await Windows.Storage.FileIO.AppendTextAsync(gameLog, ActionBar2.Text + "\n");
                teamWBall = 1;
            }
        }

        public void TheAlgorithm()
        {
            if (teamWBall.Equals(0))
            {
                ActionBar.Text = awayOnFloor[whichAwayPlayerShoots()].ToString() + " takes a " + ChosenShot() + " " + whatLoc();
                addActionBarTextToGameLog();
            }
            else
            {
                ActionBar.Text = homeOnFloor[whichHomePlayerShoots()].ToString() + " takes a " + ChosenShot() + " " + whatLoc();
                addActionBarTextToGameLog();
            }
        }
    }
}