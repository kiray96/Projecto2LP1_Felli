using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class GameManager
    {
        /// <summary>
        /// Bidimensional object array that holds the game objects 
        /// </summary>
        public IGameObject[,] grid = new IGameObject[5, 3];
        private Render r;
        private Piece playingPiece;

        /// <summary>
        /// Game manager constructor & game startup
        /// </summary>
        /// <param name="r"></param>
        public GameManager(Render r)
        {
            this.r = r;

            //Cycle through the bidimensional array
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Populate the grid with the square objects 
                    grid[i, j] = new Square(PlayableType.nonPlayable, i, j);
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
            grid[0, 0] = new Piece(0, 0, 1, PieceColor.black);
            grid[0, 1] = new Piece(0, 1, 2, PieceColor.black);
            grid[0, 2] = new Piece(0, 2, 3, PieceColor.black);
            grid[1, 0] = new Piece(1, 0, 4, PieceColor.black);
            grid[1, 1] = new Piece(1, 1, 5, PieceColor.black);
            grid[1, 2] = new Piece(1, 2, 6, PieceColor.black);

            //Spawn the white pieces
            grid[3, 0] = new Piece(3, 0, 4, PieceColor.white);
            grid[3, 1] = new Piece(3, 1, 5, PieceColor.white);
            grid[3, 2] = new Piece(3, 2, 6, PieceColor.white);
            grid[4, 0] = new Piece(4, 0, 1, PieceColor.white);
            grid[4, 1] = new Piece(4, 1, 2, PieceColor.white);
            grid[4, 2] = new Piece(4, 2, 3, PieceColor.white);

            //Spawn the middle empty square
            grid[2, 1] = new Square(PlayableType.playable, 2, 1);
        }
        public void GameLoop()
        {

        }

        private void SelectPlayingPiece(string input)
        {
            Piece p;

            foreach(IGameObject go in grid)
            {
                
            }

        }
    }
}

