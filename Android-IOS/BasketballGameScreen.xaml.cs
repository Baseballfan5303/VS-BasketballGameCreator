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
    public partial class BasketballGameScreen : ContentPage
    {

        public static ArrayList awayTeamPlayersX = new ArrayList();
        public static ArrayList homeTeamPlayersX = new ArrayList();
        public static ArrayList awayOnFloor = new ArrayList();
        public static ArrayList homeOnFloor = new ArrayList();

        public static int ballFirst, ATPlayer, HTPlayer, jumpShot, layUp, postShot, shotLoc, atScore, htScore, whichShot, scoreIncrement, important, ftSender, numFT;
        public static int xyz;

        public static object teamWBall, FTSenderXYZ; //teamWBall: away = 0, home = 1 ... ^^whichShot: j = 0,4 , pm = 2, l = 1,3

        public static string FINALGAMELOG;
        public static string ATNameXYZ, HTNameXYZ;

        public BasketballGameScreen()
        {
            this.InitializeComponent();

            gameStart();
            //SetUpImage();
        }

        /*private void SetUpImage()
        {
            string filename = "Mobile-BasketballGameCreator.Assets.basketballcourt3.png";
            mainImage.Source = ImageSource.FromFile(filename);
        }*/

        public void gameStart()
        {
            FINALGAMELOG = "";

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

            awayStartersA.Text = PreGameSetup.getATName().Substring(0, 3);
            homeStartersA.Text = PreGameSetup.getHTName().Substring(0, 3);
            awayBenchA.Text = PreGameSetup.getATName().Substring(0, 3);
            homeBenchA.Text = PreGameSetup.getHTName().Substring(0, 3);

            currentQuarter.Text = "N/A"; xyz = 0;

            atScore = 0; htScore = 0; scoreIncrement = 0; teamWBall = whoGetsBallFirst(); important = 0; ftSender = 0; numFT = 0;
        }

        private async void gameOver_Click(object sender, EventArgs e)
        {
            FINALGAMELOG += "The game is over." + "\n";
            FINALGAMELOG += "The quarters were " + (PreGameSetup.getUserInputTime() / 60) + " minutes long." + "\n";
            FINALGAMELOG += ATNameT.Text + "- Score: " + atScore.ToString() + "." + "\n";
            FINALGAMELOG += HTNameT.Text + "- Score: " + htScore.ToString() + "." + "\n";

            await Navigation.PushAsync(new GameLogScreen());
        }

        public static string getFINALGAMELOG()
        { return FINALGAMELOG; }
        public static string getAwayTeamNameForGL()
        { return ATNameXYZ; }
        public static string getHomeTeamNameForGL()
        { return HTNameXYZ; }

        public void addActionBarTextToGameLog()
        {
            FINALGAMELOG += ActionBar.Text + "\n";
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
        { return HTPlayer; }

        public static string ChosenShot()
        {
            Random w = new Random(); whichShot = w.Next(0, 5);
            if (whichShot == 0 || whichShot == 4)
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
            else if (whichShot == 2)
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
            //clearCircleColors();
            if (whichShot == 0 || whichShot == 4)
            {
                Random a1 = new Random(); shotLoc = a1.Next(0, 140);
                if (shotLoc < 5)
                { //mle.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid left elbow."; }
                else if (shotLoc > 4 && shotLoc < 10)
                { //tle.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the high left elbow."; }
                else if (shotLoc > 9 && shotLoc < 15)
                { //mre.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid right elbow."; }
                else if (shotLoc > 14 && shotLoc < 20)
                { //tre.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the high right elbow."; }
                else if (shotLoc > 19 && shotLoc < 25)
                { //lbl.Color = Xamarin.Forms.Color.Green;
                    scoreIncrement = 2; 
                    return "from the left baseline."; }
                else if (shotLoc > 24 && shotLoc < 30)
                { //rbl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the right baseline."; }
                else if (shotLoc > 29 && shotLoc < 45)
                { //lct.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the left corner three."; }
                else if (shotLoc > 44 && shotLoc < 60)
                { //rct.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the right corner three."; }
                else if (shotLoc > 59 && shotLoc < 75)
                { //mlt.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the left mid three."; }
                else if (shotLoc > 74 && shotLoc < 90)
                { //mrt.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the right mid three."; }
                else if (shotLoc > 89 && shotLoc < 105)
                { //tlt.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the left top three."; }
                else if (shotLoc > 104 && shotLoc < 120)
                { //trt.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the right top three."; }
                else if (shotLoc > 119 && shotLoc < 135)
                { //tmt.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 3; 
                    return "from the top three."; }
                else
                { //ftl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the free throw line."; }
            }
            else if (whichShot == 2)
            {
                Random a2 = new Random(); shotLoc = a2.Next(0, 75);
                if (shotLoc < 15)
                { //llb.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the left block."; }
                else if (shotLoc > 14 && shotLoc < 20)
                { //mle.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid left elbow."; }
                else if (shotLoc > 19 && shotLoc < 25)
                { //tle.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the high left elbow."; }
                else if (shotLoc > 24 && shotLoc < 40)
                { //lrb.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the right block."; }
                else if (shotLoc > 39 && shotLoc < 45)
                { //mre.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid right elbow."; }
                else if (shotLoc > 44 && shotLoc < 50)
                { //tre.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the high right elbow."; }
                else if (shotLoc > 49 && shotLoc < 60)
                { //lbl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the left baseline."; }
                else if (shotLoc > 59 && shotLoc < 70)
                { //rbl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the right baseline."; }
                else
                { //ftl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the free throw line."; }
            }
            else
            {
                Random a3 = new Random(); shotLoc = a3.Next(0, 70);
                if (shotLoc < 15)
                { //llb.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the left block."; }
                else if (shotLoc > 14 && shotLoc < 30)
                { //lrb.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the right block."; }
                else if (shotLoc > 29 && shotLoc < 40)
                { //mle.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid left elbow."; }
                else if (shotLoc > 39 && shotLoc < 50)
                { //mre.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the mid right elbow."; }
                else if (shotLoc > 49 && shotLoc < 60)
                { //lbl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the left baseline."; }
                else
                { //rbl.Color = Xamarin.Forms.Color.Green; 
                    scoreIncrement = 2; 
                    return "from the right baseline."; }
            }
        }

        private void StartQuarter_Click(object sender, EventArgs e)
        {
            if (sender.Equals(startQ1))
            {
                FINALGAMELOG += "Q1 begins." + "\n";
                TheAlgorithm();
                startQ1.IsVisible = false;
                currentQuarter.Text = "Q1";
                xyz = PreGameSetup.getUserInputTime();
                theTimer();
            }
            else if (sender.Equals(startQ2))
            {
                FINALGAMELOG += "Q1 ends, Q2 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n";
                TheAlgorithm();
                startQ2.IsVisible = false;
                currentQuarter.Text = "Q2";
                xyz = PreGameSetup.getUserInputTime();
                theTimer();
            }
            else if (sender.Equals(startQ3))
            {
                FINALGAMELOG += "Q2 ends, Q3 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n";
                TheAlgorithm();
                startQ3.IsVisible = false;
                currentQuarter.Text = "Q3";
                xyz = PreGameSetup.getUserInputTime();
                theTimer();
            }
            else
            {
                FINALGAMELOG += "Q3 ends, Q4 begins. Scoring update: " + PreGameSetup.getATName().Substring(0, 3) + " " + atScore.ToString() + " " + PreGameSetup.getHTName().Substring(0, 3) + " " + htScore.ToString() + "\n";
                TheAlgorithm();
                startQ4.IsVisible = false;
                currentQuarter.Text = "Q4";
                xyz = PreGameSetup.getUserInputTime();
                theTimer();
            }
        }

        private async void theTimer()
        {
            /*Device.StartTimer(TimeSpan.FromSeconds(xyz), () =>
            {
                if (xyz != 0)
                {
                    int min = (int)(xyz / 60);
                    int sec = (int)(xyz % 60);
                    if (sec < 10)
                        uiTimer.Text = "0" + min.ToString() + ":" + "0" + sec.ToString();
                    else
                        uiTimer.Text = "0" + min.ToString() + ":" + sec.ToString();
                    return true;
                }
                else
                {
                    uiTimer.Text = "Done.";
                    return false;
                }
            });*/
            while(xyz != 0)
            {
                await Task.Delay(1000);
                xyz--;
                int min = (int)(xyz / 60);
                int sec = (int)(xyz % 60);
                if (sec < 10)
                    uiTimer.Text = "0" + min.ToString() + ":" + "0" + sec.ToString();
                else
                    uiTimer.Text = "0" + min.ToString() + ":" + sec.ToString();

            }
            uiTimer.Text = "Done.";
        }

        private void ChangeAwayFloor_Click(object sender, EventArgs e)
        {
            if (sender.Equals(awayStartersA))
            {
                AOFP1.Text = awayTeamPlayersX[5].ToString(); AOFP2.Text = awayTeamPlayersX[6].ToString(); AOFP3.Text = awayTeamPlayersX[7].ToString(); AOFP4.Text = awayTeamPlayersX[8].ToString(); AOFP5.Text = awayTeamPlayersX[9].ToString();
                awayOnFloor[0] = awayTeamPlayersX[5].ToString(); awayOnFloor[1] = awayTeamPlayersX[6].ToString(); awayOnFloor[2] = awayTeamPlayersX[7].ToString(); awayOnFloor[3] = awayTeamPlayersX[8].ToString(); awayOnFloor[4] = awayTeamPlayersX[9].ToString();
                FINALGAMELOG += ATNameT.Text + "'s bench was brought in." + "\n";
                TheAlgorithm();
            }
            else if (sender.Equals(awayBenchA))
            {
                AOFP1.Text = awayTeamPlayersX[0].ToString(); AOFP2.Text = awayTeamPlayersX[1].ToString(); AOFP3.Text = awayTeamPlayersX[2].ToString(); AOFP4.Text = awayTeamPlayersX[3].ToString(); AOFP5.Text = awayTeamPlayersX[4].ToString();
                awayOnFloor[0] = awayTeamPlayersX[0].ToString(); awayOnFloor[1] = awayTeamPlayersX[1].ToString(); awayOnFloor[2] = awayTeamPlayersX[2].ToString(); awayOnFloor[3] = awayTeamPlayersX[3].ToString(); awayOnFloor[4] = awayTeamPlayersX[4].ToString();
                FINALGAMELOG += ATNameT.Text + "'s starters were brought in." + "\n";
                TheAlgorithm();
            }
        }
        private void ChangeHomeFloor_Click(object sender, EventArgs e)
        {
            if (sender.Equals(homeStartersA))
            {
                HOFP1.Text = homeTeamPlayersX[5].ToString(); HOFP2.Text = homeTeamPlayersX[6].ToString(); HOFP3.Text = homeTeamPlayersX[7].ToString(); HOFP4.Text = homeTeamPlayersX[8].ToString(); HOFP5.Text = homeTeamPlayersX[9].ToString();
                homeOnFloor[0] = homeTeamPlayersX[5].ToString(); homeOnFloor[1] = homeTeamPlayersX[6].ToString(); homeOnFloor[2] = homeTeamPlayersX[7].ToString(); homeOnFloor[3] = homeTeamPlayersX[8].ToString(); homeOnFloor[4] = homeTeamPlayersX[9].ToString();
                FINALGAMELOG += HTNameT.Text + "'s bench was brought in." + "\n";
                TheAlgorithm();
            }
            else if (sender.Equals(homeBenchA))
            {
                HOFP1.Text = homeTeamPlayersX[0].ToString(); HOFP2.Text = homeTeamPlayersX[1].ToString(); HOFP3.Text = homeTeamPlayersX[2].ToString(); HOFP4.Text = homeTeamPlayersX[3].ToString(); HOFP5.Text = homeTeamPlayersX[4].ToString();
                homeOnFloor[0] = homeTeamPlayersX[0].ToString(); homeOnFloor[1] = homeTeamPlayersX[1].ToString(); homeOnFloor[2] = homeTeamPlayersX[2].ToString(); homeOnFloor[3] = homeTeamPlayersX[3].ToString(); homeOnFloor[4] = homeTeamPlayersX[4].ToString();
                FINALGAMELOG += HTNameT.Text + "'s starters were brought in." + "\n";
                TheAlgorithm();
            }
        }

        private void MakeButton_Click(object sender, EventArgs e)
        {
            ActionBar2.IsVisible = false;
            shotScenarios();
            if (teamWBall.Equals(0) && important != 4 && important != 5)
            {
                atScore += scoreIncrement;
                ATScoreT.Text = atScore.ToString();
                teamWBall = 1;
                FINALGAMELOG += awayOnFloor[getWAPS()].ToString() + " made the shot." + "\n";
                TheAlgorithm();
            }
            else if (teamWBall.Equals(1) && important != 4 && important != 5)
            {
                htScore += scoreIncrement;
                HTScoreT.Text = htScore.ToString();
                teamWBall = 0;
                FINALGAMELOG += homeOnFloor[getWHPS()].ToString() + " made the shot." + "\n";
                TheAlgorithm();
            }
            else
            {
                if (teamWBall.Equals(0))
                {
                    atScore += scoreIncrement;
                    ATScoreT.Text = atScore.ToString();
                    FINALGAMELOG += awayOnFloor[getWAPS()].ToString() + " made the shot." + "\n";
                }
                else if (teamWBall.Equals(1))
                {
                    htScore += scoreIncrement;
                    HTScoreT.Text = htScore.ToString();
                    FINALGAMELOG += homeOnFloor[getWHPS()].ToString() + " made the shot." + "\n";
                }
                somethingHappenedOnMake();
            }
        }

        private void MissButton_Click(object sender, EventArgs e)
        {
            ActionBar2.IsVisible = false;
            shotScenarios();
            if (teamWBall.Equals(0) && important != 0 && important != 9 && important != 4 && important != 5)
            {
                teamWBall = 1;
                FINALGAMELOG += awayOnFloor[getWAPS()].ToString() + " missed the shot." + "\n";
                TheAlgorithm();
            }
            else if (teamWBall.Equals(1) && important != 0 && important != 9 && important != 4 && important != 5)
            {
                teamWBall = 0;
                FINALGAMELOG += homeOnFloor[getWHPS()].ToString() + " missed the shot." + "\n";
                TheAlgorithm();
            }
            else
            {
                somethingHappenedOnMiss();
            }
        }

        private void MakeFTButton_Click(object sender, EventArgs e)
        {
            if (FTSenderXYZ.Equals(0))
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.IsVisible = false;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    atScore++;
                    ATScoreT.Text = atScore.ToString();
                    teamWBall = 1;
                }
                else
                {
                    ActionBar2.IsVisible = false;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    htScore++;
                    HTScoreT.Text = htScore.ToString();
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
                        ActionBar2.IsVisible = true;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        atScore++;
                        ATScoreT.Text = atScore.ToString();
                    }
                    else
                    {
                        ActionBar2.IsVisible = true;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        htScore++;
                        HTScoreT.Text = htScore.ToString();
                    }
                    numFT--;
                }
                else
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.IsVisible = false;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " made the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        atScore++;
                        ATScoreT.Text = atScore.ToString();
                        teamWBall = 1;
                    }
                    else
                    {
                        ActionBar2.IsVisible = false;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " made the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        htScore++;
                        HTScoreT.Text = htScore.ToString();
                        teamWBall = 0;
                    }
                    TheAlgorithm();
                }
            }
        }

        private void MissFTButton_Click(object sender, EventArgs e)
        {
            if (FTSenderXYZ.Equals(0))
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.IsVisible = false;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    teamWBall = 1;
                }
                else
                {
                    ActionBar2.IsVisible = false;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                    FINALGAMELOG += ActionBar2.Text + "\n";
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
                        ActionBar2.IsVisible = true;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                    }
                    else
                    {
                        ActionBar2.IsVisible = true;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                    }
                    numFT--;
                }
                else
                {
                    if (teamWBall.Equals(0))
                    {
                        ActionBar2.IsVisible = false;
                        ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " missed the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        teamWBall = 1;
                    }
                    else
                    {
                        ActionBar2.IsVisible = false;
                        ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " missed the free throw.";
                        FINALGAMELOG += ActionBar2.Text + "\n";
                        teamWBall = 0;
                    }
                    TheAlgorithm();
                }
            }
        }

        public void somethingHappenedOnMiss()
        {
            if (important == 0 || important == 9)
            {
                if (teamWBall.Equals(0))
                {
                    ActionBar2.IsVisible = true;
                    ActionBar2.Text = awayOnFloor[whichAwayPlayerShoots()].ToString() + " got the offensive rebound. ";
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    teamWBall = 0;
                    TheAlgorithm();
                }
                else
                {
                    ActionBar2.IsVisible = true;
                    ActionBar2.Text = homeOnFloor[whichHomePlayerShoots()].ToString() + " got the offensive rebound. ";
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    teamWBall = 1;
                    TheAlgorithm();
                }
            }
            else if (important == 4 || important == 5)
            {
                numFT = scoreIncrement;
                FTSenderXYZ = 1;
                if (teamWBall.Equals(0))
                {
                    ActionBar2.IsVisible = true;
                    ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " gets " + scoreIncrement + " free throws.";
                    //clearCircleColors();
                    //ftl.Color = Xamarin.Forms.Color.Green;
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    teamWBall = 0;
                }
                else
                {
                    ActionBar2.IsVisible = true;
                    ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " gets " + scoreIncrement + " free throws.";
                    //clearCircleColors();
                    //ftl.Color = Xamarin.Forms.Color.Green;
                    FINALGAMELOG += ActionBar2.Text + "\n";
                    teamWBall = 1;
                }
            }
        }

        public void somethingHappenedOnMake()
        {
            numFT = 1;
            FTSenderXYZ = 0;
            if (teamWBall.Equals(0))
            {
                ActionBar2.IsVisible = true;
                ActionBar2.Text = awayOnFloor[getWAPS()].ToString() + " gets 1 free throw.";
                //clearCircleColors();
                //ftl.Color = Xamarin.Forms.Color.Green;
                FINALGAMELOG += ActionBar2.Text + "\n";
                teamWBall = 0;
            }
            else
            {
                ActionBar2.IsVisible = true;
                ActionBar2.Text = homeOnFloor[getWHPS()].ToString() + " gets 1 free throw.";
                //clearCircleColors();
                //ftl.Color = Xamarin.Forms.Color.Green;
                FINALGAMELOG += ActionBar2.Text + "\n";
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