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
        public object[,] grid = new object[5, 5];
        private bool player;
        private Black black1;
        private Black black2;
        private Black black3;
        private Black black4;
        private Black black5;
        private Black black6;
        private White white1;
        private White white2;
        private White white3;
        private White white4;
        private White white5;
        private White white6;
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
            SpawnEntities();

            GameLoop();
        }

        /// <summary>
        /// Spawn of the entities in the grid
        /// </summary>
        private void SpawnEntities()
        {
            //Spawn the black pieces
            grid[0, 0] = new Black(0, 0, 1);
            grid[0, 2] = new Black(0, 2, 2);
            grid[0, 4] = new Black(0, 4, 3);
            grid[1, 1] = new Black(1, 1, 4);
            grid[1, 2] = new Black(1, 2, 5);
            grid[1, 3] = new Black(1, 3, 6);

            //Spawn the white pieces
            grid[3, 1] = new White(3, 1, 1);
            grid[3, 2] = new White(3, 2, 2);
            grid[3, 3] = new White(3, 3, 3);
            grid[4, 0] = new White(7, 0, 4);
            grid[4, 2] = new White(7, 0, 5);
            grid[4, 4] = new White(7, 0, 6);

            black1 = (Black)grid[0, 0];
            black2 = (Black)grid[0, 2];
            black3 = (Black)grid[0, 4];
            black4 = (Black)grid[1, 1];
            black5 = (Black)grid[1, 2];
            black6 = (Black)grid[1, 3];

            white1 = (White)grid[3, 1];
            white2 = (White)grid[3, 2];
            white3 = (White)grid[3, 3];
            white4 = (White)grid[4, 0];
            white5 = (White)grid[4, 2];
            white6 = (White)grid[4, 4];
        }
        public void GameLoop()
        {

        }
    }
}

