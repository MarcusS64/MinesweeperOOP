using System.Reflection.Metadata;

namespace Minesweeper;

public class Minefield
{
    private bool[,] _bombLocations = new bool[5, 5];
    public string[,] minefieldState = new string[5, 5];
    private string[,] solutionState = new string[5, 5];
    private static (int x, int y)[] directions = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

    public void SetBomb(int x, int y)
    {
        _bombLocations[x, y] = true;
    }

    public void SetMinefieldMap()
    {
        for (int i = 0; i < minefieldState.GetLength(0); i++)
        {
            for(int j = 0; j < minefieldState.GetLength(1); j++)
            {
                minefieldState[i, j] = "?";

                if (_bombLocations[i, j] == true)
                {
                    solutionState[i, j] = "X";
                }
                else
                {
                    int numberOfNeighbourBombs = 0;
                    foreach (var direction in directions)
                    {
                        int _x = i - direction.x;
                        int _y = j - direction.y;
                        if (_x < minefieldState.GetLength(0) && _y < minefieldState.GetLength(1) && _x >= 0 && _y >= 0) //If not then there is no square to connect
                        {
                            if (_bombLocations[_x, _y] == true)
                            {
                                numberOfNeighbourBombs++;
                            }
                        }
                    }

                    if (numberOfNeighbourBombs > 0)
                    {
                        solutionState[i, j] = numberOfNeighbourBombs.ToString();
                    }
                    else
                    {
                        solutionState[i, j] = " ";
                    }
                }
            }
        }
    }


    public bool FoundBomb(int x, int y)
    {
        if (_bombLocations[x, y] == true)
        {
            return true;
        }
        return false;
    }

    public void UpdateMinefieldMap(int x, int y) //Use BFS search to traverse the field
    {

        minefieldState[x, y] = solutionState[x, y];
        if (solutionState[x, y] == " ")
        {
            foreach (var direction in directions)
            {
                int _x = x - direction.x;
                int _y = y - direction.y;
                if (_x < minefieldState.GetLength(0) && _y < minefieldState.GetLength(1) && _x >= 0 && _y >= 0)
                {
                    if (minefieldState[_x, _y] == "?")
                    {
                        UpdateMinefieldMap(_x, _y);
                    }

                }
            }
        }
    }

    public bool CheckIfAllBombsUncovered()
    {
        for (int i = 0; i < minefieldState.GetLength(0); i++)
        {
            for (int j = 0; j < minefieldState.GetLength(1); j++)
            {
                if (minefieldState[i, j] == "?" && _bombLocations[i, j] == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int GetRowLength()
    {
        return minefieldState.GetLength(0);
    }

    public bool IsTileUncovered(int x, int y)
    {
        if (minefieldState[x, y] == "?")
        {
            return false;
        }
        return true;
    }

    public void UncoverAllNonBombSpaces() //Only for testing purposes
    {
        for (int i = 0; i < minefieldState.GetLength(0); i++)
        {
            for (int j = 0; j < minefieldState.GetLength(1); j++)
            {
                if (minefieldState[i, j] == "?" && _bombLocations[i, j] == false)
                {
                    minefieldState[i, j] = solutionState[i, j];
                }
            }
        }
    }
}
