using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class Square
    {
        public Direction[] PossibleMovements { get; set; }
        public PlayableType Type { get; private set; }
        public Piece Piece { get; set; }

        public Square(PlayableType type)
        {
            Type = type;
        }
        public bool HasPiece()
        {
            if (Piece == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
