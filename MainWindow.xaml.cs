using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Symulator_ukladow_logicznych
{
    public partial class MainWindow : Window
    {
        UIElement dragObj = null;
        Point offset;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvasPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObj == null)
            {
                return;
            }

            var posObj = e.GetPosition(sender as IInputElement);

            Canvas.SetLeft(this.dragObj, posObj.X - this.offset.X);
            Canvas.SetTop(this.dragObj, posObj.Y - this.offset.Y);
        }

        private void canvasPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dragObj = null;
            this.canvas.ReleaseMouseCapture();
        }

        private void createGate(object sender, RoutedEventArgs e)
        {
            Rectangle rect = new Rectangle();

            switch ((string)((MenuItem)sender).Tag)
            {
                case "btnAND":
                    rect.Fill = Brushes.Red;
                    rect.Tag = "AND";
                    break;
                case "btnNOT":
                    rect.Fill = Brushes.LightGreen;
                    rect.Tag = "NOT";
                    break;
                case "btnNAND":
                    rect.Fill = Brushes.Yellow;
                    rect.Tag = "NAND";
                    break;
                case "btnOR":
                    rect.Fill = Brushes.Purple;
                    rect.Tag = "OR";
                    break;
                case "btnXOR":
                    rect.Fill = Brushes.DarkGreen;
                    rect.Tag = "XOR";
                    break;
            }
            rect.Width = 100;
            rect.Height = 50;

            Canvas.SetLeft(rect, 350);
            Canvas.SetTop(rect, 175);

            rect.PreviewMouseDown += rectPreviewMouseDown;

            canvas.Children.Add(rect);
        }

        private void rectPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObj = sender as UIElement;

            this.offset = e.GetPosition(this.canvas);
            this.offset.X -= Canvas.GetLeft(this.dragObj);
            this.offset.Y -= Canvas.GetTop(this.dragObj);

            this.canvas.CaptureMouse();
        }
    }
}
