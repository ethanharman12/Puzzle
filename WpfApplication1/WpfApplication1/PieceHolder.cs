using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class PieceHolder
    {
        public List<Piece> pieces = new List<Piece>();

        public PieceHolder()
        {
        }

        public PieceHolder(Piece p)
        {
            pieces.Add(p);
        }

        public void Move(Piece sender, double xdist, double ydist)
        {
            foreach (Piece p in pieces)
            {
                p.Move(sender, xdist, ydist);
            }
        }

        public void Snap(Piece p)
        {
            foreach (Piece ps in p.ph.pieces)
            {
                if (!p.Equals(ps))
                {
                    ps.ph = this;
                    pieces.Add(ps);
                }
            }
            p.ph = this;
            pieces.Add(p);
        }
    }
}
