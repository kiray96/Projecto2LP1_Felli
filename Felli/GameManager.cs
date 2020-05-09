using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class GameManager
    {
        /// <summary>
        /// Bidimensional object array that holds the game objects 
        /// </summary>
        public object[,] grid = new object[8, 8];
        private bool player;
        private Black black;
        private White white;
        private Render r;

        /// <summary>
        /// Game manager constructor & game startup
        /// </summary>
        /// <param name="r"></param>
        public GameManager(Render r)
        {
            this.r = r;
            player = false;

            //Cycle through the bidimensional array
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //populate the grid with the square objects 
                    grid[i, j] = new Square();
                }
            }

            //SpawnEntities();
            //GameLoop();
        }
        public void GameLoop()
        {

        }
    }
}

