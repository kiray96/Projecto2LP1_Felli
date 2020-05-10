using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class Black
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
        /// Sheep unic Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Create Sheep values
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="id"></param>
        public Black(int row, int column, int id)
        {
            Row = row;
            Column = column;
            Id = id;
        }
    }
}
