using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;
using Flame.Games;
using Flame.Geometry;
using Flame.Debug;

namespace Fantactics
{
    class GameGrid
    {
        private Cell[,] _cellMap;
        public GameGrid(Game game, int columns, int rows)
        {
            Game = game;
            CellSize = 64;
            Columns = columns;
            Rows = rows;

            _cellMap = new Cell[columns,rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Cell c = new Cell(this, i, j);
                    Game.Add(c);
                    _cellMap[i, j] = c;
                }
            }
        }
        public int Rows { get; }
        public int Columns { get; }
        public int CellSize { get; set; }
        public Game Game { get; }

        public static void LoadAssets(Game game)
        {
            game.Assets.LoadTexture("Assets/cell.png", "cell");
        }

        public Cell GetCell(int column, int row)
        {
            return _cellMap[column, row];
        }

        public Cell GetCellFromPosition(Vector position)
        {
            int column = (int)Math.Round(position.X / CellSize);
            int row = (int)Math.Round(position.Y / CellSize);
            return _cellMap[column,row];
        }

        public Vector GetPositionFromCell(int column, int row)
        {
            double x = column * CellSize;
            double y = row * CellSize;

            return new Vector(x, y);
        }
        public void DebugCells()
        {
            foreach(Cell cell in _cellMap)
            {
                cell.Opacity.Value = 1;
            }
        }
        public void Seed(int column, int row, string seedId, int radius)
        {
            // just textures for now
            int startColumn = column - radius;
            int startRow = row - radius;

            for (int i = startColumn; i < column + radius; i++)
            {
                for (int j = startRow; j < row + radius; j++)
                {
                    if (i < 0 || i >= Columns || j < 0 || j >= Rows)
                    {
                        continue;
                    }
                    //GetCell(i, j).Terrain.BindToTexture(seedId);
                }
            }
        }
        public byte[,] ToBytes()
        {
            int l1 = _cellMap.GetLength(0);
            int l2 = _cellMap.GetLength(1);
            byte[,] theByes = new byte[l1, l2];

            for (int i = 0; i < l1; i++)
            {
                for (int j = 0; j < l2; j++)
                {
                    theByes[i,j] = 1;
                }
            }

            return theByes;
        }
    }

    class Cell : Sprite
    {
        private GameGrid _grid;
        public Cell(GameGrid grid, int column, int row): base(grid.Game, column * grid.CellSize, (row * grid.CellSize))
        {
            _grid = grid;
            BindToTexture("cell");
            Rectangle.Width = grid.CellSize;
            Rectangle.Height = grid.CellSize;

            //Terrain = new Terrain(grid.Game, (int)Position.X, (int)Position.Y);
            Opacity.Value = 1;  
        }
        //public Terrain  Terrain { get; }
    }
}
