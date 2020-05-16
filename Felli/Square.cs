
namespace Felli
{
    /// <summary>
    ///Square class
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Possible moves property
        /// </summary>
        public Direction[] PossibleMovements { get; set; }

        /// <summary>
        /// Type of pices 
        /// </summary>
        public PlayableType Type { get; private set; }

        /// <summary>
        /// Pieces property
        /// </summary>
        public Piece Piece { get; set; }


        /// <summary>
        /// Construct the square object with the respective playable Type
        /// </summary>
        /// <param name="type"></param>
        public Square(PlayableType type)
        {
            Type = type;
        }

        /// <summary>
        /// Check if has a piece
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// To check if the direction is possible
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool HasDirection(Direction dir)
        {
            foreach (Direction d in PossibleMovements)
            {
                if (d == dir) return true;
            }

            return false;
        }
    }
}
