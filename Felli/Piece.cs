using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class Piece : IGameObject
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
        public int PreviousColumn { get; private set; }

        /// <summary>
        /// Previous row position
        /// </summary>
        public int PreviousRow { get; private set; }

        /// <summary>
        /// Piece Id
        /// </summary>
        public int Id { get; private set; }

        public PieceColor Color { get; private set; }


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
                case Direction.NorthEast:
                    Column++;
                    Row--;
                    break;

                case Direction.North:
                    Row--;
                    break;

                case Direction.NorthWest:
                    Column--;
                    Row--;
                    break;

                case Direction.East:
                    Column++;
                    break;

                case Direction.West:
                    Column--;
                    break;

                case Direction.SouthEast:
                    Column++;
                    Row++;
                    break;

                case Direction.South:
                    Row++;
                    break;

                case Direction.SouthWest:
                    Column--;
                    Row++;
                    break;
            }
        }

        /// <summary>
        /// Undo the last move made (sheep)
        /// </summary>
        public void ResetMovement()
        {
            Row = PreviousRow;
            Column = PreviousColumn;
        }
    }
}
