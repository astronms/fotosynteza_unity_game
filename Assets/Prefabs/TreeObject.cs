using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject
{
    private TreeClr _treeColor;
    private TreeLvl _treeLevel;

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

    public TreeLvl TreeLevel
    {
        get
        {
            return _treeLevel;
        }
        set
        {
            _treeLevel = value;
        }
    }

    public TreeClr TreeColor
    {
        get
        {
            return _treeColor;
        }
        set
        {
            _treeColor = value;
        }
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
