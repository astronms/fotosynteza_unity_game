using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject
{
    public TreeLvl _treeLevel { get; private set; }
    public Player _player { get; set; } // wskaźnik do gracza

    public TreeObject(TreeLvl treelevel, Player player)
    {
        _treeLevel = treelevel;
        _player = player;
    }

    public enum TreeLvl
    {
        SEED,
        SMALL,
        MID,
        BIG
    }

    public void LvlUp()
    {
        if (_treeLevel < (TreeLvl) 3)
        {
            _treeLevel += 1;
        }
    }
}
