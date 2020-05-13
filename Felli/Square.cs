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

        public bool HasDirection(Direction dir)
        {
            foreach (Direction d in PossibleMovements)
            {
                if (d == dir) return true;
            }

            return false;
        }

        /// <summary>
        /// Method that gets similar directions based on the given direction
        /// </summary>
        /// <returns></returns>
        public Direction GetSimilarDirection(Direction dir)
        {
            string s = "";
            Direction d = default;

            if (dir.ToString().Contains("N")) s = "N";
            else if (dir.ToString().Contains("S")) s = "S";



            foreach (Direction direction in PossibleMovements)
            {
                if (direction.ToString().Contains(s))
                {
                    d = direction;
                    break;
                }
            }

            return d;
        }

        /// <summary>
        /// Method that gives similar directions
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool HasSimilarDirection(Direction dir)
        {
            string s = "";

            if (dir.ToString().Contains("N")) s = "N";
            else if (dir.ToString().Contains("S")) s = "S";


            foreach (Direction d in PossibleMovements)
            {
                if (d.ToString().Contains(s)) return true;
            }

            return false;
        }
    }
}
