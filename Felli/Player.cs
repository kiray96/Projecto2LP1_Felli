
namespace Felli
{
    /// <summary>
    /// Player's class 
    /// </summary>
    public class Player
    {
        public PieceColor Color { get; private set; }

        /// <summary>
        /// Piece Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Piece Count
        /// </summary>
        public int PieceCount { get; set; }

        /// <summary>
        /// Create player values
        /// </summary>
        /// <param name="color"></param>
        /// <param name="id"></param>
        public Player(PieceColor color, int id)
        {
            Color = color;
            Id = id;
            PieceCount = 6;
        }
    }
}
