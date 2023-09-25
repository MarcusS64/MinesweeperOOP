using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Minesweeper;

    

public class MineFieldGraph
{
    public MineFieldNode[,] squares;
    int width;
    int height;
    private static (int x, int y)[] coords = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

    public MineFieldGraph(int width, int height)
    {
        squares = new MineFieldNode[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                squares[i, j] = new MineFieldNode(i, j);
            }
        }

        this.width = width;
        this.height = height;

        //ConnectEverySquare(width, height);
    }

    public void SetBomb(int x, int y)
    {
        squares[x, y].setBomb();
    }

    public void ConnectEverySquare()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < coords.Length; k++)//Loops through all the squares in the 8 directions around the square
                {
                    int x = i - coords[k].x;
                    int y = j - coords[k].y;
                    if (x < width && y < height && x >= 0 && y >= 0) //If not then there is no square to connect
                    {
                        squares[i, j].SetAdjacentSquare(squares[x, y]);
                    }
                }
                squares[i, j].setUncoveredIcon();
            }
        }
    }

    public bool FoundBomb(int x, int y)
    {
        return squares[x, y].isBomb;
    }

    public void UpdateMinefieldMap(int x, int y) //Use BFS search to traverse the field
    {

        squares[x, y].UncoverTile();
        if (squares[x, y].GetUncoverIcon() == " ")
        {
            foreach (var direction in coords)
            {
                int _x = x - direction.x;
                int _y = y - direction.y;
                if (_x < squares.GetLength(0) && _y < squares.GetLength(1) && _x >= 0 && _y >= 0)
                {
                    if (!squares[_x, _y].IsUncovered())
                    {
                        UpdateMinefieldMap(_x, _y);
                    }
                }
            }
        }
    }

    public bool CheckIfAllBombsUncovered()
    {
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                if (!squares[i, j].IsUncovered() && squares[i, j].isBomb == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int GetWidth()
    {
        return width;
    }


    public bool IsTileUncovered(int x, int y)
    {
        if (!squares[x, y].IsUncovered())
        {
            return false;
        }
        return true;
    }
}

