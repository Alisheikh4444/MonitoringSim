using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BLL.Methods;

namespace MonitoringSim
{
    public partial class MainWindow : Window
    {
        private readonly NumberGenerator _gen1 = new(16.7, 0, 100);
        private readonly NumberGenerator _gen2 = new(16.7, 200, 900);

        private const double D1MinOk = 30, D1MaxOk = 70;
        private const double D2MinOk = 450, D2MaxOk = 750;

        private readonly Brush OkBrush = new SolidColorBrush(Color.FromRgb(16, 185, 129));
        private readonly Brush WarnBrush = new SolidColorBrush(Color.FromRgb(239, 68, 68));

        public MainWindow()
        {
            InitializeComponent();

        }
    }
}
