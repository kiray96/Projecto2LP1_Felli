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
            r.MainMenu();
            r.Draw(gm.grid);
            gm.SetPlayers();
            gm.GameLoop();
        }
    }
}
