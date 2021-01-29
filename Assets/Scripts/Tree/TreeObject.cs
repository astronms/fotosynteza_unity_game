using System;

[Serializable]
public class TreeObject
{
    public enum TreeLvl
    {
        SEED,
        SMALL,
        MID,
        BIG
    }

    public TreeObject(TreeLvl treelevel, Player player)
    {
        _treeLevel = treelevel;
        _player = player;
    }

    public TreeLvl _treeLevel { get; private set; }
    public Player _player { get; } // wskaźnik do gracza

    public void LvlUp()
    {
        if (_treeLevel < (TreeLvl) 3) _treeLevel += 1;
    }
}