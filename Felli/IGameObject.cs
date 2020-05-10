using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public interface IGameObject
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
