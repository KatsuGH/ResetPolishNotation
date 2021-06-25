using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using AdvancedCalculate.WPF;
using OPZ_Mekin;

namespace OPZWPF
{
    public partial class MainWindow : Window
    {
        public bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CoordinateAxis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (flag)
            {
                coordinateAxes.Children.Clear();
                new FunctionGraphDrawer(coordinateAxes, startText, endText);
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            var opz = new ReverseReader();
            var res = opz.Reverse(functionText.Text.ToCharArray().Where(x=>x!=' ').ToArray());
            var sb = new StringBuilder();
            foreach (var item in res)
            {
                sb.Append(item);
            }
            rpnText.Text = sb.ToString();
            AddingResults(rpnText.Text.ToCharArray());
            flag = true;
        }

        private void AddingResults(char[] func)
        {
            var s = new StepX(int.Parse(StepText.Text),int.Parse(startText.Text), 
                int.Parse(startText.Text), int.Parse(endText.Text), func);
            var table = s.GetTable();
            resultesGrid.ItemsSource = table;
        }

        private void ButtonZoomPlus(object sender, RoutedEventArgs e)
        {
            coordinateAxes.Children.Clear();
            Values.ValueZoom += 5;
            coordinateAxes.Height += 5;
            coordinateAxes.Width += 5;
            new FunctionGraphDrawer(coordinateAxes, startText, endText);
        }

        private void ButtonZoomMinus(object sender, RoutedEventArgs e)
        {
            coordinateAxes.Children.Clear();
            Values.ValueZoom -= 5;
            coordinateAxes.Height -= (double)5;
            coordinateAxes.Width -= 5;
            new FunctionGraphDrawer(coordinateAxes, startText, endText);
        }
    }
}

