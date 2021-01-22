using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Id { get; set; }
    public PlayerType PlayerType { get; set; }
    public string Nick { get; set; }
    public int Points { get; set; }
    public int PointOfLIghts { get; set; }
    public int NumberOfSeeds { get; set; }
    public int NumberOfSmallTrees { get; set; }
    public int NumberOfMediumTrees { get; set; }
    public int NumberOfLargeTrees { get; set; }

    public Player(int id, string nick, PlayerType playerType)
    {
        Nick = nick;
        PlayerType = playerType;
        Points = 0;
        PointOfLIghts = 0;
        NumberOfSeeds = 2;
        NumberOfSmallTrees = 4;
        NumberOfMediumTrees = 1;
        NumberOfLargeTrees = 0;
    }
    public bool buy()
    {
        return true;
    }

    public bool plant()
    {
        return false;
    }

}

