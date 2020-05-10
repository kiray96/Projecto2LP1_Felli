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
    }
}
