using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    private TreeClr _treeColor { get; set; }
    private TreeLvl _treeLevel { get; set; }

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
