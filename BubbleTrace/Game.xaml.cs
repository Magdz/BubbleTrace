using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BubbleTrace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private bool EndGame = false;

        private Color ColorRed = Color.FromArgb(255, 255, 0, 0);
        private Color ColorBlue = Color.FromArgb(255, 0, 0, 255);
        private Color ColorGray = Color.FromArgb(255, 128, 128, 128);
        private Color ColorEmpty = Color.FromArgb(255, 255, 255, 255);
        private Color Color;
        private Ellipse[] Circles;

        int[] traverse = new int[] { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45 };
        int[] reverse = new int[] { 0, 2, 5, 9, 14, 20, 27, 35, 44, 54 };

        bool[] Rows = new bool[10];
        bool[] Left = new bool[10];
        bool[] Right = new bool[10];

        public Game()
        {
            this.InitializeComponent();
            Color = ColorRed;
            Circles = new Ellipse[] { Circle1, Circle2, Circle3, Circle4, Circle5, Circle6, Circle7, Circle8, Circle9, Circle10,
                Circle11, Circle12, Circle13, Circle14, Circle15, Circle16, Circle17, Circle18, Circle19, Circle20,
                Circle21, Circle22, Circle23, Circle24, Circle25, Circle26, Circle27, Circle28, Circle29, Circle30,
                Circle31, Circle32, Circle33, Circle34, Circle35, Circle36, Circle37, Circle38, Circle39, Circle40,
                Circle41, Circle42, Circle43, Circle44, Circle45, Circle46, Circle47, Circle48, Circle49, Circle50,
                Circle51, Circle52, Circle53, Circle54, Circle55};
        }

        private void Circle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ellipse Circle = (Ellipse)sender;
            SolidColorBrush solidBrush = new SolidColorBrush();
            if(((SolidColorBrush)(Circle.Fill)).Color == ColorEmpty)
            {
                solidBrush.Color = Color;
                Circle.Fill = solidBrush;
                if (Color == ColorRed) Color = ColorBlue;
                else Color = ColorRed;
                if(!EndGame)CheckPoints();
            }
        }

        private void CheckPoints()
        {
            for (int i = 0; i < traverse.Length; ++i) 
            {
                if (!Rows[i]) Rows[i] = CheckRow(traverse[i], i + 1);
                if (!Left[i]) Left[i] = CheckDiagonal(traverse[i], 10 - i, i + 1);
                if (!Right[i]) Right[i] = CheckDiagonal(reverse[i], 10 - i, i);
            }
            for(int i = 0; i < traverse.Length; ++i)
            {
                EndGame = true;
                if (!Rows[i] || !Left[i] || !Right[i])
                {
                    EndGame = false;
                    break;
                }
            }
            if (EndGame)
            {
                SolidColorBrush solidBrush = new SolidColorBrush();
                if(int.Parse(RedScore.Text) > int.Parse(BlueScore.Text))
                {
                    WinText.Text = "Red Player Wins";
                    solidBrush.Color = ColorRed;
                }
                else
                {
                    WinText.Text = "Blue Player Wins";
                    solidBrush.Color = ColorBlue;
                }
                WinText.Foreground = solidBrush;
                PlayAgianButton.IsEnabled = true;
                Thickness margin = MainMenuButton.Margin;
                margin.Right += 150;
                MainMenuButton.Margin = margin;
            }
        }

        private bool CheckRow(int start, int length)
        {
            for (int i = start; i < start + length; ++i)
            {
                if (((SolidColorBrush)(Circles[i].Fill)).Color == ColorEmpty) return false;
            }
            for (int i = start; i < start + length; ++i)
            {
                SolidColorBrush solidBrush = new SolidColorBrush();
                solidBrush.Color = ColorGray;
                Circles[i].Fill = solidBrush;
            }
            AddScore(length);
            return true;
        }

        private bool CheckDiagonal(int start, int length, int increment)
        {
            int TempInc = increment;
            int toCheck = start;
            for (int i = 0; i < length; ++i, toCheck += increment) 
            {
                if (((SolidColorBrush)(Circles[toCheck].Fill)).Color == ColorEmpty) return false;
                increment++;
            }
            increment = TempInc;
            toCheck = start;
            for (int i = 0; i < length; ++i, toCheck += increment)
            {
                SolidColorBrush solidBrush = new SolidColorBrush();
                solidBrush.Color = ColorGray;
                Circles[toCheck].Fill = solidBrush;
                increment++;
            }
            AddScore(length);
            return true;
        }

        private void AddScore(int Score)
        {
            if (Color == ColorBlue)
            {
                RedScore.Text = (int.Parse(RedScore.Text) + Score).ToString();
            }
            else
            {
                BlueScore.Text = (int.Parse(BlueScore.Text) + Score).ToString();
            }
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PlayAgianButton_Click(object sender, RoutedEventArgs e)
        {
            EndGame = false;
            WinText.Text = "";
            BlueScore.Text = "0";
            RedScore.Text = "0";
            Rows = new bool[10];
            Left = new bool[10];
            Right = new bool[10];
            SolidColorBrush solidBrush = new SolidColorBrush();
            solidBrush.Color = ColorEmpty;
            foreach(Ellipse Circle in Circles)
            {
                Circle.Fill = solidBrush;
            }
            PlayAgianButton.IsEnabled = false;
            Thickness margin = MainMenuButton.Margin;
            margin.Right -= 150;
            MainMenuButton.Margin = margin;
        }
    }
}
