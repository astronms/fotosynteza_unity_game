using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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
        public int NumberOfLArgeTrees { get; set; }

        public Player(int id, string nick, PlayerType playerType)
        {
            Nick = nick;
            PlayerType = playerType;
            Points = 0;
            PointOfLIghts = 1;
            NumberOfSeeds = 6;
            NumberOfSmallTrees = 8;
            NumberOfMediumTrees = 4;
            NumberOfLArgeTrees = 2;
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
}

