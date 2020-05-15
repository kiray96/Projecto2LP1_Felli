using System;

namespace Felli
{
    /// <summary>
    /// Main Class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Create game variables
            Render r = new Render();
            GameManager gm = new GameManager(r);
            r.Draw(gm.grid);
            //r.MainMenu();
            gm.GameLoop();
        }
    }
}
