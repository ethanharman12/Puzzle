using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName;
        int pieces = 4;
        int snapRange = 15;
        int snapCount = 0;
        int square = 360;
        Piece[][] puzzle;
        Image totalImage = new Image();
        bool isPlaying = false;
        Random rand = new Random();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CircleSetup(int i, int j, CroppedBitmap cb)
        {
            if (puzzle[i][j].N == 1 || puzzle[i][j].N == -1)
            {
                puzzle[i][j].circles[0] = new Ellipse();
                puzzle[i][j].circles[0].Height = (square / pieces) / 5;
                puzzle[i][j].circles[0].Width = (square / pieces) / 5;
                puzzle[i][j].circles[0].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                puzzle[i][j].circles[0].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;                
                if (puzzle[i][j].N == 1)
                {
                    puzzle[i][j].circles[0].Margin = new Thickness(puzzle[i][j].X + (square / pieces) * .4, puzzle[i][j].Y - (square / pieces) * .2, 0, 0);
                    puzzle[i][j].circles[0].Fill = new ImageBrush(cb);
                }
                else
                {
                    puzzle[i][j].circles[0].Margin = new Thickness(puzzle[i][j].X + (square / pieces) * .4, puzzle[i][j].Y, 0, 0);
                    puzzle[i][j].circles[0].Fill = new SolidColorBrush(Colors.BlanchedAlmond);
                }
                Grid1.Children.Add(puzzle[i][j].circles[0]);
            }
            if (puzzle[i][j].S == 1 || puzzle[i][j].S == -1)
            {
                puzzle[i][j].circles[1] = new Ellipse();
                puzzle[i][j].circles[1].Height = (square / pieces) / 5;
                puzzle[i][j].circles[1].Width = (square / pieces) / 5;
                puzzle[i][j].circles[1].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                puzzle[i][j].circles[1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;                
                if (puzzle[i][j].S == 1)
                {
                    puzzle[i][j].circles[1].Margin = new Thickness(puzzle[i][j].X + (square / pieces) * .4, puzzle[i][j].Y + (square / pieces), 0, 0);
                    puzzle[i][j].circles[1].Fill = new ImageBrush(cb);
                }
                else
                {
                    puzzle[i][j].circles[1].Margin = new Thickness(puzzle[i][j].X + (square / pieces) * .4, puzzle[i][j].Y + (square / pieces) * .8, 0, 0);
                    puzzle[i][j].circles[1].Fill = new SolidColorBrush(Colors.BlanchedAlmond);
                }
                Grid1.Children.Add(puzzle[i][j].circles[1]);
            }
            if (puzzle[i][j].E == 1 || puzzle[i][j].E == -1)
            {
                puzzle[i][j].circles[2] = new Ellipse();
                puzzle[i][j].circles[2].Height = (square / pieces) / 5;
                puzzle[i][j].circles[2].Width = (square / pieces) / 5;
                puzzle[i][j].circles[2].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                puzzle[i][j].circles[2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;                
                if (puzzle[i][j].E == 1)
                {
                    puzzle[i][j].circles[2].Margin = new Thickness(30 + (square / pieces) * (j + 1), 30 + (square / pieces) * (i + .4), 0, 0);
                    puzzle[i][j].circles[2].Fill = new ImageBrush(cb);
                }
                else
                {
                    puzzle[i][j].circles[2].Margin = new Thickness(30 + (square / pieces) * (j + .8), 30 + (square / pieces) * (i + .4), 0, 0);
                    puzzle[i][j].circles[2].Fill = new SolidColorBrush(Colors.BlanchedAlmond);
                }
                Grid1.Children.Add(puzzle[i][j].circles[2]);
            }
            if (puzzle[i][j].W == 1 || puzzle[i][j].W == -1)
            {
                puzzle[i][j].circles[3] = new Ellipse();
                puzzle[i][j].circles[3].Height = (square / pieces) / 5;
                puzzle[i][j].circles[3].Width = (square / pieces) / 5;
                puzzle[i][j].circles[3].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                puzzle[i][j].circles[3].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;                
                if (puzzle[i][j].W == 1)
                {
                    puzzle[i][j].circles[3].Margin = new Thickness(30 + (square / pieces) * (j - .2), 30 + (square / pieces) * (i + .4), 0, 0);
                    puzzle[i][j].circles[3].Fill = new ImageBrush(cb);
                }
                else
                {
                    puzzle[i][j].circles[3].Margin = new Thickness(30 + (square / pieces) * j, 30 + (square / pieces) * (i + .4), 0, 0);
                    puzzle[i][j].circles[3].Fill = new SolidColorBrush(Colors.BlanchedAlmond);
                }
                Grid1.Children.Add(puzzle[i][j].circles[3]);
            }
        }

        private void RandomCut(int i, int j)
        {
            if (i == 0)
            {
                puzzle[i][j].N = 0;
                puzzle[i][j].S = rand.Next(0,2) < 1 ? -1 : 1;
            }
            else if (i == pieces - 1)
            {
                puzzle[i][j].N = puzzle[i-1][j].S * -1;
                puzzle[i][j].S = 0;
            }
            else
            {
                puzzle[i][j].N = puzzle[i - 1][j].S * -1;
                puzzle[i][j].S = rand.Next(0, 2) < 1 ? -1 : 1;
            }

            if (j == 0)
            {
                puzzle[i][j].W = 0;
                puzzle[i][j].E = rand.Next(0, 2) < 1 ? -1 : 1;
            }
            else if (j == pieces - 1)
            {
                puzzle[i][j].W = puzzle[i][j - 1].E * -1;
                puzzle[i][j].E = 0;
            }
            else
            {
                puzzle[i][j].W = puzzle[i][j - 1].E * -1;
                puzzle[i][j].E = rand.Next(0, 2) < 1 ? -1 : 1;
            }
            //if (puzzle[i][j].N == 1)
            //    MessageBox.Show(" ");
        }

        private void Scatter()
        {
            for (int i = 0; i < pieces; i++)
            {
                for (int j = 0; j < pieces; j++)
                {
                    puzzle[i][j].X = rand.Next(30, 689 - square/pieces);
                    puzzle[i][j].Y = rand.Next(30, 434 - square/pieces);
                }
            }
        }

        private void Snap(PieceHolder pHold) //closest only
        {
            bool snapped = false;
            double xdist;
            double ydist;

            foreach (Piece p in pHold.pieces)
            {
                if (p.j != 0)
                {
                    if (!pHold.pieces.Contains(puzzle[p.i][p.j - 1]))
                    {
                        if (Math.Abs((puzzle[p.i][p.j - 1].X + square / pieces) - p.X) < snapRange && Math.Abs(puzzle[p.i][p.j - 1].Y - p.Y) < snapRange)
                        {
                            xdist = (puzzle[p.i][p.j - 1].X + square / pieces - p.X) * -1;
                            ydist = (puzzle[p.i][p.j - 1].Y - p.Y) * -1;
                            pHold.Move(new Piece(), xdist, ydist);
                            p.ph.Snap(puzzle[p.i][p.j - 1]);
                            snapped = true;
                        }
                    }
                }
                if (p.j != pieces - 1 && !snapped)
                {
                    if (!pHold.pieces.Contains(puzzle[p.i][p.j + 1]))
                    {
                        if (Math.Abs((puzzle[p.i][p.j + 1].X - square / pieces) - p.X) < snapRange && Math.Abs(puzzle[p.i][p.j + 1].Y - p.Y) < snapRange)
                        {
                            xdist = (puzzle[p.i][p.j + 1].X - square / pieces - p.X) * -1;
                            ydist = (puzzle[p.i][p.j + 1].Y - p.Y) * -1;                            
                            pHold.Move(new Piece(), xdist, ydist);
                            p.ph.Snap(puzzle[p.i][p.j + 1]);
                            snapped = true;
                        }
                    }
                }

                if (p.i != 0 && !snapped)
                {
                    if (!pHold.pieces.Contains(puzzle[p.i - 1][p.j]))
                    {
                        if (Math.Abs(puzzle[p.i - 1][p.j].X - p.X) < snapRange && Math.Abs((puzzle[p.i - 1][p.j].Y + square / pieces) - p.Y) < snapRange)
                        {
                            xdist = (puzzle[p.i - 1][p.j].X - p.X) * -1;
                            ydist = (puzzle[p.i - 1][p.j].Y + square / pieces - p.Y) * -1;
                            pHold.Move(new Piece(), xdist, ydist);
                            p.ph.Snap(puzzle[p.i - 1][p.j]);
                            snapped = true;
                        }
                    }
                }
                if (p.i != pieces - 1 && !snapped)
                {
                    if (!pHold.pieces.Contains(puzzle[p.i + 1][p.j]))
                    {
                        if (Math.Abs(puzzle[p.i + 1][p.j].X - p.X) < snapRange && Math.Abs((puzzle[p.i + 1][p.j].Y - square / pieces) - p.Y) < snapRange)
                        {
                            xdist = (puzzle[p.i + 1][p.j].X - p.X) * -1;
                            ydist = (puzzle[p.i + 1][p.j].Y - square / pieces - p.Y) * -1;
                            pHold.Move(new Piece(), xdist, ydist);
                            p.ph.Snap(puzzle[p.i + 1][p.j]);
                            snapped = true;
                        }
                    }
                }
                if (snapped)
                {
                    break;
                }
            }

            if (snapped)
            {
                snapCount++;
                if (snapCount == (pieces * pieces) - 1)
                {
                    isPlaying = false;
                    snapCount = 0;
                    for (int i = 0; i < pieces; i++)
                    {
                        for (int j = 0; j < pieces; j++)
                        {
                            puzzle[i][j].img.StrokeThickness = 0;
                        }
                    }
                    MessageBox.Show("Puzzle Completed!");
                }
            }            
        }

        private void Browser_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png)|*.jpg;*.bmp;*.gif;*.png";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                fileName = ofd.FileName;
                textBox1.Text = fileName;
                Preview.Source = new BitmapImage(new Uri(fileName));
                Cutter.IsEnabled = true;            
            }
        }

        private void Cutter_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                if (MessageBox.Show("A puzzle is in progress.  Start new puzzle?", "New Puzzle", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    isPlaying = false;
                }
            }

            if (!isPlaying)
            {
                if (puzzle != null)
                {
                    for (int i = 0; i < puzzle.Length; i++)
                    {
                        for (int j = 0; j < puzzle[i].Length; j++)
                        {
                            puzzle[i][j].img.Visibility = System.Windows.Visibility.Hidden;
                        }
                    }
                }

                if (radioButton1.IsChecked == true)
                {
                    pieces = 4;
                }
                else if (radioButton2.IsChecked == true)
                {
                    pieces = 6;
                }
                else
                {
                    pieces = 8;
                }
                puzzle = new Piece[pieces][];

                for (int i = 0; i < pieces; i++)
                {
                    puzzle[i] = new Piece[pieces];
                }

                BitmapSource bs = new BitmapImage(new Uri(fileName));
                double ht = bs.PixelHeight - 1;
                double wd = bs.PixelWidth - 1;

                for (int i = 0; i < pieces; i++)
                {
                    for (int j = 0; j < pieces; j++)
                    {
                        puzzle[i][j] = new Piece(i, j);
                        RandomCut(i, j);
                        puzzle[i][j].img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                        puzzle[i][j].img.MouseUp += new MouseButtonEventHandler(img_MouseUp);
                        puzzle[i][j].img.Height = square / pieces;
                        puzzle[i][j].img.Width = square / pieces;
                        puzzle[i][j].img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        puzzle[i][j].img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        puzzle[i][j].X = 30 + (square / pieces) * j;
                        puzzle[i][j].Y = 30 + (square / pieces) * i;
                        puzzle[i][j].img.Stroke = new SolidColorBrush(Colors.Gray);
                        puzzle[i][j].img.StrokeThickness = 1;

                        CroppedBitmap cb = new CroppedBitmap(new BitmapImage(new Uri(fileName)), new Int32Rect((int)wd / pieces * j, (int)ht / pieces * i, (int)wd / pieces, (int)ht / pieces));                        
                        puzzle[i][j].img.Fill = new ImageBrush(cb);
                        Grid1.Children.Add(puzzle[i][j].img);
                        //CircleSetup(i, j, cb);                  
                    }
                }
                Scatter();
                isPlaying = true;                
            }
        }

        public void img_MouseDown(object sender, MouseEventArgs e)
        {
            int tempi = int.Parse(((Rectangle)(sender)).Name[1].ToString());
            int tempj = int.Parse(((Rectangle)(sender)).Name[2].ToString());
            foreach (Piece p in puzzle[tempi][tempj].ph.pieces)
            {
                Grid1.Children.Remove(p.img);
                Grid1.Children.Add(p.img);
                foreach (Ellipse c in p.circles)
                {
                    if (c != null)
                    {
                        Grid1.Children.Remove(c);
                        Grid1.Children.Add(c);
                    }
                }
            }
            Grid1.UpdateLayout();
        }

        public void img_MouseUp(object sender, MouseEventArgs e)
        {
            int tempi = int.Parse(((Rectangle)(sender)).Name[1].ToString());
            int tempj = int.Parse(((Rectangle)(sender)).Name[2].ToString());
            Snap(puzzle[tempi][tempj].ph);
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {            
            Preview.Visibility = System.Windows.Visibility.Visible;                      
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            Preview.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
