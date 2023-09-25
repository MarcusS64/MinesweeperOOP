using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Minesweeper
{
    public class MineFieldNode
    {
        #region Properties
        int x, y;
        Point point;
        public bool isBomb;
        public List<MineFieldNode> adjacentSquares;
        string uncoveredIcon;
        string coveredIcon;
        string activeIcon;
        bool uncovered;
        #endregion

        public MineFieldNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            point = new Point(x, y); //Could remove/replace this
            adjacentSquares = new List<MineFieldNode>();
            coveredIcon = "?";
            uncovered = false;
            activeIcon = coveredIcon;
            uncoveredIcon = " "; //Set as default here in the constructor
        }

        public int X()
        {
            return x;
        }

        public int Y()
        {
            return y;
        }

        public void SetAdjacentSquare(MineFieldNode square)
        {
            adjacentSquares.Add(square);
        }

        public void setBomb()
        {
            isBomb = true;
            uncoveredIcon = "X";
        }

        public void setUncoveredIcon()
        {
            int neighbourBombs = 0;
            foreach (var neighbour in adjacentSquares)
            {
                if (neighbour.isBomb)
                {
                    neighbourBombs++;
                }
            }

            if(neighbourBombs > 0) 
            {
                uncoveredIcon = neighbourBombs.ToString();
            }            
        }

        public bool IsUncovered()
        {
            return uncovered;
        }

        public void UncoverTile()
        {
            activeIcon = uncoveredIcon;
            uncovered = true;
        }

        public string GetUncoverIcon()
        {
            return uncoveredIcon;
        }

        public string GetActiveIcon()
        {
            return activeIcon;
        }

    }
}
