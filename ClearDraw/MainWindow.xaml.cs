using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ClearDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool _LightOrDark;
        Line newLine;
        Point start;
        Point end;
        Point currentPoint = new Point();
        Point _startPoint = new Point();
        Point _endPoint = new Point();

        private Color currentColor;
        bool _writeEnabled = true;
        string _shape = "pen";


        System.Windows.Threading.DispatcherTimer _hideTimer;


        public MainWindow()
        {
            currentColor = Colors.Black;
            _LightOrDark = true;
            InitializeComponent();
            Focus();

            cvs.Children.Clear();
            InkCanvas inkCanvas = new InkCanvas();
            inkCanvas.Name = "InkCanvas";
            inkCanvas.Background = Brushes.White;
            inkCanvas.Opacity = .01;
            inkCanvas.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            inkCanvas.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            Mouse.OverrideCursor = Cursors.Arrow;
            cvs.Children.Add(inkCanvas);

            _hideTimer = new System.Windows.Threading.DispatcherTimer();
            _hideTimer.Tick += dispatcherTimer_Tick;
            _hideTimer.Interval = new TimeSpan(0, 0, 2);


        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z)
            {
                Application.Current.Shutdown();
            }
            else if (e.Key == Key.X)
            {
                cvs.Children.Clear();
                InkCanvas inkCanvas = new InkCanvas();
                inkCanvas.Name = "InkCanvas";
                inkCanvas.Background = Brushes.White;
                inkCanvas.Opacity = .01;
                inkCanvas.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                inkCanvas.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                Mouse.OverrideCursor = Cursors.Arrow;
                cvs.Children.Add(inkCanvas);
            }
            else if (e.Key == Key.C)
            {
                if (_LightOrDark == true)
                {
                    currentColor = Colors.White;
                    _LightOrDark = false;
                }
                else if (_LightOrDark == false)
                {
                    currentColor = Colors.Black;
                    _LightOrDark = true;
                }
            }
            else if (e.Key == Key.V)
            {
                cvs.Width = 0;
                cvs.Height = 0;
                inkCanvas.Height = 0;
                inkCanvas.Width = 0;
                cvs.Visibility = Visibility.Hidden;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_writeEnabled)
            {
                
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (_shape == "pen")
                    {
                        cvsButtons.Visibility = Visibility.Collapsed;
                        Mouse.OverrideCursor = Cursors.Pen;

                        Line line = new Line();

                        line.StrokeThickness = 3;
                        

                        line.Stroke = new SolidColorBrush(currentColor);
                        line.Stroke.Opacity = 1;

                        line.X1 = currentPoint.X;
                        line.Y1 = currentPoint.Y;
                        line.X2 = e.GetPosition(this).X;
                        line.Y2 = e.GetPosition(this).Y;

                        currentPoint = e.GetPosition(this);

                        cvs.Children.Add(line);
                    }
                    else if (_shape == "arrow")
                    {
                        
                    }
                }
                else
                {
                    cvsButtons.Visibility = Visibility.Visible;
                    currentPoint = e.GetPosition(this);
                }
            }
            else
            {
                currentPoint = e.GetPosition(this);
            }
        }

        //private void cvs_MouseDown_1(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ButtonState == MouseButtonState.Pressed)
        //        currentPoint = e.GetPosition(this);

        //        pup.IsOpen = false;

        //    if (_shape == "arrow")
        //    {
        //        _startPoint = e.GetPosition(this);


        //    }

        //}

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ButtonState == MouseButtonState.Pressed)
        //        currentPoint = e.GetPosition(this);



        //}
        private void Window_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;

        }

        //private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ButtonState == MouseButtonState.Pressed)
        //        currentPoint = e.GetPosition(this);

        //    if (_shape == "arrow")
        //    {
        //        _startPoint = e.GetPosition(this);


        //    }
            
                
            
        //}

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cvs.Children.Clear();
            InkCanvas inkCanvas = new InkCanvas();
            inkCanvas.Name = "InkCanvas";
            inkCanvas.Background = Brushes.White;
            inkCanvas.Opacity = .01;
            inkCanvas.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            inkCanvas.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            Mouse.OverrideCursor = Cursors.Arrow;
            cvs.Children.Add(inkCanvas);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            _writeEnabled = false;
            currentColor = clrCanvas.SelectedColor.Value;
        }

        private void cvs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;

            if (_shape == "arrow")
            {
                _endPoint = e.GetPosition(this);

                Line line = new Line();

                line.StrokeThickness = 3;

                line.Stroke = new SolidColorBrush(currentColor);
                line.Stroke.Opacity = 1;

                line.X1 = _startPoint.X;
                line.Y1 = _startPoint.Y;
                line.X2 = _endPoint.X;
                line.Y2 = _endPoint.Y;
                cvs.Children.Add(line);
            }
        }

        private void DetailsButton_Checked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _writeEnabled = false;
            cvs.Visibility = Visibility.Hidden;
            cvsButtons.Visibility = Visibility.Collapsed;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            cvs.Visibility = Visibility.Visible;
            _writeEnabled = true;
        }

        private void pup_LostFocus(object sender, RoutedEventArgs e)
        {
            pup.IsOpen = false;
            _writeEnabled = true;
        }

        private void pup_Closed(object sender, EventArgs e)
        {
            _writeEnabled = true;
        }

        private void cvsTouch_MouseEnter(object sender, MouseEventArgs e)
        {
            cvsButtons.Visibility = Visibility.Visible;
        }

        private void btnPen_Click(object sender, RoutedEventArgs e)
        {
            _shape = "pen";
        }

        private void btnArrow_Click(object sender, RoutedEventArgs e)
        {
            _shape = "arrow";
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {
            _shape = "text";
        }

        private void cvsButtons_MouseLeave(object sender, MouseEventArgs e)
        {
            _hideTimer.Start();
            
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!_writeEnabled)
            {
                cvsButtons.Visibility = Visibility.Collapsed;
                _hideTimer.Stop();
            }
            
        }

        private void cvsButtons_MouseEnter(object sender, MouseEventArgs e)
        {
            _hideTimer.Stop();
        }

        private void pupShapes_MouseEnter(object sender, MouseEventArgs e)
        {
            _hideTimer.Stop();
        }
    }
}
