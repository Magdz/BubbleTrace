using Windows.UI;
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
        private Color ColorRed = Color.FromArgb(255, 255, 0, 0);
        private Color ColorBlue = Color.FromArgb(255, 0, 0, 255);
        private Color ColorEmpty = Color.FromArgb(255, 244, 244, 245);
        private Color Color;
        public Game()
        {
            this.InitializeComponent();
            Color = ColorRed;
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
            }
        }
    }
}
