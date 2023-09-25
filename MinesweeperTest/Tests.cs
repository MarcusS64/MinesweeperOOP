using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestMethodOutOfBounds()
    {
        var field = new MineFieldGraph(5, 5);

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.ConnectEverySquare();
        string playerGuess = "05";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

    [TestMethod]
    public void TestMethodNotANumber()
    {
        var field = new MineFieldGraph(5, 5);

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.ConnectEverySquare();
        string playerGuess = "0a";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

    [TestMethod]
    public void TestMethodSamePositionGuess()
    {
        var field = new MineFieldGraph(5, 5);

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.ConnectEverySquare();
        field.UpdateMinefieldMap(4, 0);
        string playerGuess = "40";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

    [TestMethod]
    public void TestMethodCheckVictoryCondition()
    {
        var field = new MineFieldGraph(5, 5);

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.ConnectEverySquare();
        field.UpdateMinefieldMap(0, 2);
        field.UpdateMinefieldMap(0, 3);
        field.UpdateMinefieldMap(0, 4);
        field.UpdateMinefieldMap(1, 0);
        field.UpdateMinefieldMap(1, 2);
        field.UpdateMinefieldMap(1, 3);
        field.UpdateMinefieldMap(2, 0);
        field.UpdateMinefieldMap(2, 1);
        field.UpdateMinefieldMap(2, 2);
        field.UpdateMinefieldMap(2, 3);
        field.UpdateMinefieldMap(2, 4);
        field.UpdateMinefieldMap(3, 0);
        field.UpdateMinefieldMap(3, 1);
        field.UpdateMinefieldMap(3, 2);
        field.UpdateMinefieldMap(3, 3);
        field.UpdateMinefieldMap(3, 4);
        field.UpdateMinefieldMap(4, 0);
        field.UpdateMinefieldMap(4, 1);
        field.UpdateMinefieldMap(4, 3);
        field.UpdateMinefieldMap(4, 4);
        Assert.IsTrue(field.CheckIfAllBombsUncovered());
    }
}
