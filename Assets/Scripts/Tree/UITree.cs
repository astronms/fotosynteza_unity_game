using UnityEngine;

public class UITree : MonoBehaviour
{
    //This class is needed to keep Trees between scenes. 

    public static UITree Instance { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void removeAllTrees()
    {
        GameObject Trees = GameObject.Find("/Trees");
        int childs = Trees.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) DestroyImmediate(Trees.transform.GetChild(i).gameObject);
    }
}