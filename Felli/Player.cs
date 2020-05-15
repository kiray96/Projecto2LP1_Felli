

using System.Dynamic;

namespace Felli
{
    public class Player
    {
        public PieceColor Color { get; private set; }

        /// <summary>
        /// Piece Id
        /// </summary>
        public int Id { get; private set; }

        public int PieceCount { get; set; }

        public Player(PieceColor color, int id)
        {
            Color = color;
            Id = id;
            PieceCount = 6;
        }
    }
}
