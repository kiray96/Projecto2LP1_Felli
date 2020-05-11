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
        public Square[,] grid = new Square[5, 3];
        private Render r;
        private Piece playingPiece;
        private Player p1, p2;
        private PieceColor turnColor;


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
                    grid[i, j] = new Square(PlayableType.playable);
                }
            }
            SpawnEntities();
            SetPossibleMovements();
            GameLoop();
        }

        /// <summary>
        /// Spawn of the entities in the grid
        /// </summary>
        private void SpawnEntities()
        {
            //Spawn the black pieces
            grid[0, 0].Piece = new Piece(0, 0, 1, PieceColor.black);
            grid[0, 1].Piece = new Piece(0, 1, 2, PieceColor.black);
            grid[0, 2].Piece = new Piece(0, 2, 3, PieceColor.black);
            grid[1, 0].Piece = new Piece(1, 0, 4, PieceColor.black);
            grid[1, 1].Piece = new Piece(1, 1, 5, PieceColor.black);
            grid[1, 2].Piece = new Piece(1, 2, 6, PieceColor.black);

            //Spawn the white pieces
            grid[3, 0].Piece = new Piece(3, 0, 4, PieceColor.white);
            grid[3, 1].Piece = new Piece(3, 1, 5, PieceColor.white);
            grid[3, 2].Piece = new Piece(3, 2, 6, PieceColor.white);
            grid[4, 0].Piece = new Piece(4, 0, 1, PieceColor.white);
            grid[4, 1].Piece = new Piece(4, 1, 2, PieceColor.white);
            grid[4, 2].Piece = new Piece(4, 2, 3, PieceColor.white);

            //Identifying non-playable positions for rendering purposes
            grid[2, 0] = new Square(PlayableType.nonPlayable);
            grid[2, 2] = new Square(PlayableType.nonPlayable);

        }
        private void GameLoop()
        {
            string input = null;

            while (input != "W" && input != "B")
            {
                Console.Clear();
                r.ShowPlayerSelection();
                input = Console.ReadLine().ToUpper();
            }

            turnColor = p1.Color;

            while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6")
            {
                if (CheckWin())
                {
                    break;
                }

                playingPiece = null;
                                
                while (playingPiece == null)
                {
                    Console.Clear();
                    r.Draw(grid);
                    r.ShowSelectPieceText();
                    input = Console.ReadLine();
                    SelectPlayingPiece(input);

                }

                if (CheckMovment())
                {

                }
                

            }


        }

        private void ChangeTurnColor()
        {
            if (turnColor == PieceColor.white)
            {
                turnColor = PieceColor.black;
            }
            else
            {
                turnColor = PieceColor.white;
            }
        }


        private void SelectPlayer(string s)
        {
            switch (s)
            {
                case "W":
                    p1 = new Player(PieceColor.white, 1);
                    p2 = new Player(PieceColor.black, 2);
                    break;
                case "B":
                    p1 = new Player(PieceColor.black, 1);
                    p2 = new Player(PieceColor.white, 2);
                    break;
            }
        }

        private void SelectPlayingPiece(string input)
        {
            
            foreach (Square go in grid)
            {
                //Defenir a peça 
                if (go.Piece.Id == Convert.ToInt32(input) && go.Piece.Color == turnColor)
                {
                    playingPiece = go.Piece;
                }
                else
                {
                    r.InvalidPieceText();
                }
            }

        }

        private void SetPossibleMovements()
        {
            grid[0, 0].PossibleMovements 
                = new Direction[] { Direction.E, Direction.SE };
            grid[0, 1].PossibleMovements 
                = new Direction[] { Direction.S, Direction.E, Direction.W };
            grid[0, 2].PossibleMovements 
                = new Direction[] { Direction.W, Direction.SW };
            grid[1, 0].PossibleMovements 
                = new Direction[] { Direction.NW, Direction.E, Direction.SE };
            grid[1, 1].PossibleMovements 
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[1, 2].PossibleMovements 
                = new Direction[] { Direction.NE, Direction.W, Direction.SW };
            grid[2, 1].PossibleMovements 
                = new Direction[] { Direction.NE, Direction.N, Direction.NW,
                    Direction.SW, Direction.S, Direction.SE };
            grid[3, 0].PossibleMovements 
                = new Direction[] { Direction.NE, Direction.E, Direction.SW };
            grid[3, 1].PossibleMovements 
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[3, 2].PossibleMovements 
                = new Direction[] { Direction.NW, Direction.W, Direction.SE };
            grid[4, 0].PossibleMovements 
                = new Direction[] { Direction.NE, Direction.E };
            grid[4, 1].PossibleMovements 
                = new Direction[] { Direction.W, Direction.E, Direction.N };
            grid[4, 2].PossibleMovements 
                = new Direction[] { Direction.W, Direction.NW };
        }

        private bool CheckWin()
        {
            if (p1.Color == turnColor)
            {
                if (p1.PieceCount == 0)
                {
                    r.Player2Win();
                    return true;
                }
            }
            else
            {
                if (p2.PieceCount == 0)
                {
                    r.Player1Win();
                    return true;
                }
            }
            return false;
        }

        private bool CheckMovment()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    if (grid[i, j].Piece.Id == playingPiece.Id && grid[i, j].Piece.Color == playingPiece.Color)
                    {                       
                        while (true)
                        {
                            r.PlayerMove(grid[i, j].PossibleMovements);
                            string selectedMovement = Console.ReadLine();
                            foreach(Direction d in grid[i, j].PossibleMovements)
                            {
                                if (Direction.IsDefined(typeof(Direction), selectedMovement.ToUpper()))
                                {
                                    if (selectedMovement.ToUpper() == d.ToString())
                                    {
                                        playingPiece.Move (d);
                                        return true;
                                    }
                                }                                
                            }
                            r.InvalidPieceText();
                        }
                    }
                }                
            }
            return false;
        }
    }
}

