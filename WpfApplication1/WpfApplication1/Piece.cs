using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace WpfApplication1
{
    class Piece
    {
        public int id;
        public int N;
        public int E;
        public int S;
        public int W;

        public int i;
        public int j;

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                this.Move(new Piece(), x - value, 0);
                x = value;
            }
        }
        private double x = 0;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                this.Move(new Piece(), 0, y - value);
                y = value;
            }
        }
        private double y = 0;
        private double oldx;
        private double oldy;
        private double newx;
        private double newy;

        public Rectangle img;

        public Ellipse[] circles;

        public PieceHolder ph;

        private bool moving = false;
        
        public Piece()
        {
        }

        public Piece(int i, int j)
        {
            this.i = i;
            this.j = j;
            ph = new PieceHolder(this);
            circles = new Ellipse[4];
            id = i + 10 * j;
            img = new Rectangle();
            img.Name = "R" + i.ToString() + j.ToString();
            img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
            img.MouseMove += new MouseEventHandler(img_MouseMove);
            img.MouseUp += new MouseButtonEventHandler(img_MouseUp);
            img.MouseLeave += new MouseEventHandler(img_MouseLeave);            
        }

        public void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            moving = true;
            oldx = e.GetPosition((IInputElement)(this.img.Parent)).X;
            oldy = e.GetPosition((IInputElement)(this.img.Parent)).Y;
        }

        public void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            moving = false;
        }

        public void img_MouseLeave(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                moving = false;
            }
        }

        public void img_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                newx = e.GetPosition((IInputElement)(this.img.Parent)).X;
                newy = e.GetPosition((IInputElement)(this.img.Parent)).Y;
                //x -= oldx - newx;
                //y -= oldy - newy;
                ph.Move(new Piece(), oldx - newx, oldy - newy);
                oldx = newx;
                oldy = newy;                
                img.Margin = new Thickness(x, y, 0, 0);
            }
        }

        public void Move(Piece sender, double xdist, double ydist)
        {
            if (!sender.Equals(this))
            {
                x -= xdist;
                y -= ydist;
                img.Margin = new Thickness(x, y, 0, 0);
                foreach (Ellipse c in circles)
                {
                    if (c != null)
                    {
                        double cx = c.Margin.Left - xdist;
                        double cy = c.Margin.Top - ydist;
                        c.Margin = new Thickness(cx, cy, 0, 0);
                    }
                }
            }
        }
    }
}
