using OPZ_Mekin;
using OPZWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedCalculate.WPF
{
    public class FunctionGraphDrawer
    {
        private double Height { get; }
        private double Width { get; }
        private Canvas FunctionGraph { get; set; }
        private Polyline PolyLine { get; set; }
        public FunctionGraphDrawer(Canvas functionGraph, TextBox startX, TextBox endX)
        {
            Height = functionGraph.ActualHeight;
            Width = functionGraph.ActualWidth;

            FunctionGraph = functionGraph;

            DrawAxes();
            
            if (StepX.AllResult.Count > 0)
            {
                GetFieldParams(double.Parse(startX.Text), double.Parse(endX.Text));
                DrawPoints();
                DrawFunction();
            }
        }
        private void DrawAxes()
        {
            FunctionGraph.Children.Add(GetLine(0, Width, Height / 2, Height / 2));
            FunctionGraph.Children.Add(GetLine(Width / 2, Width / 2, 0, Height));
            FunctionGraph.Children.Add(GetLine(Width, Width - 10, Height / 2, Height / 2 - 5));
            FunctionGraph.Children.Add(GetLine(Width, Width - 10, Height / 2, Height / 2 + 5));
            FunctionGraph.Children.Add(GetLine(Width / 2, Width / 2 - 5, 0, 10));
            FunctionGraph.Children.Add(GetLine(Width / 2, Width / 2 + 5, 0, 10));
        }
        private Line GetLine(double x1, double x2, double y1, double y2)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Aquamarine,
                StrokeThickness = 2,
            };

            return line;
        }
        private void DrawPoints()
        {
            foreach (var i in StepX.AllResult.Keys)
            {
                SetPointOfHorizontal(i);
                SetPointOfVertical(StepX.AllResult[i]); 
            }
        }
        private void SetPointOfVertical(double i)
        {
            var pointOfBottom = GetEllipse();

            Canvas.SetLeft(pointOfBottom, Width / 2 - 2);
            Canvas.SetBottom(pointOfBottom, Height / 2 + i * 100 - 3);

            FunctionGraph.Children.Add(pointOfBottom);

            SetNumberVertical(i);
        }
        private void SetPointOfHorizontal(double i)
        {
            var pointOfLeft = GetEllipse();

            Canvas.SetTop(pointOfLeft, Height / 2 - 2);
            Canvas.SetRight(pointOfLeft, Width / 2 - i * Values.ValueZoom - 3);

            FunctionGraph.Children.Add(pointOfLeft);

            SetNumberHorizontal(i);
        }
        private void SetNumberHorizontal(double i)
        {
            var num = new Label
            {
                Content = i,
                FontSize = Values.ValueZoom / 2,
            };

            Canvas.SetTop(num, Height / 2 - 2);
            Canvas.SetRight(num, Width / 2 - i * Values.ValueZoom - 5);

            FunctionGraph.Children.Add(num);

        }
        private void SetNumberVertical(double i)
        {
            var num = new Label
            {
                Content = i,
                FontSize = Values.ValueZoom / 2,
            };

            Canvas.SetLeft(num, Width / 2 - 2);
            Canvas.SetBottom(num, Height / 2 + i * Values.ValueZoom - 5);

            FunctionGraph.Children.Add(num);
        }
        private Ellipse GetEllipse()
        {
            Ellipse point = new Ellipse
            {
                Height = 4,
                Width = 4,
                Stroke = Brushes.Black,
                Fill = Brushes.Black,

            };
            return point;
        }
        private void DrawFunction()
        {
            PolyLine = new Polyline
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };

            foreach (var i in StepX.AllResult.Keys)
            {
                var point = new Point
                {
                    X = Width / 2 + i * Values.ValueZoom,
                    Y = Height / 2 - i * StepX.AllResult[i] * Values.ValueZoom,
                };
                PolyLine.Points.Add(point);
            }

            PolyLine.MouseEnter += Polyline_MouseEnter;

            FunctionGraph.Children.Add(PolyLine);
        }

        private void Polyline_MouseEnter(object sender, MouseEventArgs e)
        {
            var cursorPosition = Mouse.GetPosition(FunctionGraph);

            string[] coors = cursorPosition.ToString().Split(new char[] { ';' });

            var toolTip = new ToolTip();

            var stackCoor = new StackPanel();
            stackCoor.Children.Add(GetCoordinates(Math.Round((double.Parse(coors[0]) - Width / 2) / Values.ValueZoom, 2), "x"));
            stackCoor.Children.Add(GetCoordinates(-1 * Math.Round((double.Parse(coors[1]) - Height / 2) / Values.ValueZoom, 2), "y"));
            toolTip.Content = stackCoor;
            PolyLine.ToolTip = toolTip;
        }
        private Label GetCoordinates(double coor, string nameCoor)
        {
            var labelCoor = new Label
            {
                Content = $"{nameCoor}: {coor}"
            };

            return labelCoor;
        }

        private void GetFieldParams(double startX, double endX)
        {
            if (startX < 0)
                startX *= -1;
            if (endX < 0)
                endX *= -1;
            if (startX > endX)
                FunctionGraph.Width = startX * (Values.ValueZoom * 2) + startX * Values.ValueZoom;
            else
                FunctionGraph.Width = endX * (Values.ValueZoom * 2) + startX * Values.ValueZoom;

            double maxY = 0;

            foreach (var i in StepX.AllResult.Values)
            {
                if (i < 0)
                {
                    if (i * -1 > maxY)
                    {
                        maxY = i * -1;
                    }
                }
                else
                {
                    if (i > maxY)
                    {
                        maxY = i;
                    }
                }
            }

            FunctionGraph.Height = maxY * (Values.ValueZoom * 2) + startX * Values.ValueZoom;
        }
    }
}
