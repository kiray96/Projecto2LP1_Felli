

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

        public int PieceCount { get; private set; }

        public Player(PieceColor color, int id)
        {
            Color = color;
            Id = id;
        }

        public void ReducePieceCount()
        {
            PieceCount--;
        }
    }
}
