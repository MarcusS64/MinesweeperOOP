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
    public MineFieldNode[,] node;
    int width;
    int height;
    private static (int x, int y)[] coords = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

    public MineFieldGraph(int width, int height)
    {
        node = new MineFieldNode[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                node[i, j] = new MineFieldNode(i, j);
            }
        }

        this.width = width;
        this.height = height;

        //ConnectEverySquare(width, height);
    }

    public void SetBomb(int x, int y)
    {
        node[x, y].setBomb();
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
                        node[i, j].SetAdjacentSquare(node[x, y]);
                    }
                }
                node[i, j].setUncoveredIcon();
            }
        }
    }

    public bool FoundBomb(int x, int y)
    {
        return node[x, y].isBomb;
    }

    public void UpdateMinefieldMap(int x, int y) //Use BFS search to traverse the field
    {

        node[x, y].UncoverTile();
        if (node[x, y].GetUncoverIcon() == " ")
        {
            foreach (var direction in coords)
            {
                int _x = x - direction.x;
                int _y = y - direction.y;
                if (_x < node.GetLength(0) && _y < node.GetLength(1) && _x >= 0 && _y >= 0)
                {
                    if (!node[_x, _y].IsUncovered())
                    {
                        UpdateMinefieldMap(_x, _y);
                    }
                }
            }
        }
    }

    public bool CheckIfAllBombsUncovered()
    {
        for (int i = 0; i < node.GetLength(0); i++)
        {
            for (int j = 0; j < node.GetLength(1); j++)
            {
                if (!node[i, j].IsUncovered() && !node[i, j].isBomb)
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
        return node[x, y].IsUncovered();
    }
}

