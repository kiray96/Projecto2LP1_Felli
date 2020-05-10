using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class Square : IGameObject
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PlayableType Type { get; private set; }

        public Square(PlayableType type, int row, int column)
        {
            Row = row;
            Column = column;
            Type = type;
        }
    }
}
