using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public TreeClr _treeColor { get; set; }
    public TreeLvl _treeLevel { get; set; }

    public Player.Player _player { get; set; } // wskaźnik do gracza

    public enum TreeClr
    {
        GREEN,
        RED,
        YELLOW,
        BLUE
    }

    public enum TreeLvl
    {
        SEED,
        SMALL,
        MID,
        BIG
    }

    public void lvlUp()
    {
        if ((int)_treeColor < 3)
        {
            _treeColor += 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
