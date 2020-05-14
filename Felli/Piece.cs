using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class Piece
    {
        /// <summary>
        /// Column Property.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Row Property.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Previous column position
        /// </summary>
        public int PreviousColumn { get; set; }

        /// <summary>
        /// Previous row position
        /// </summary>
        public int PreviousRow { get; set; }

        /// <summary>
        /// Piece Id
        /// </summary>
        public int Id { get; private set; }

        public PieceColor Color { get; private set; }

        public bool IsBlocked { get; set; }

        /// <summary>
        /// Create Piece values
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="id"></param>
        public Piece(int row, int column, int id, PieceColor color)
        {
            Row = row;
            Column = column;
            Id = id;
            Color = color;
            
        }

        /// <summary>
        /// Move method of Player. Moves the Player.
        /// </summary>
        public void Move(Direction dir)
        {
            PreviousColumn = Column;
            PreviousRow = Row;

            // Move according to direction
            switch (dir)
            {
                case Direction.NE:
                    Column++;
                    Row--;
                    break;

                case Direction.N:
                    Row--;
                    break;

                case Direction.NW:
                    Column--;
                    Row--;
                    break;

                case Direction.E:
                    if (Row == 0 || Row == 4) Column += 2;
                    else Column++;
                    break;

                case Direction.W:
                    if (Row == 0 || Row == 4) Column -= 2;
                    else Column--;
                    break;

                case Direction.SE:
                    Column++;
                    Row++;
                    break;

                case Direction.S:
                    Row++;
                    break;

                case Direction.SW:
                    Column--;
                    Row++;
                    break;
            }
        }

        /// <summary>
        /// Undo the last move made
        /// </summary>
        public void ResetMovement()
        {
            Row = PreviousRow;
            Column = PreviousColumn;
        }
    }
}
