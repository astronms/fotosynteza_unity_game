using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    public static MainGameUI Instance { get; private set; }
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DontDestroyOnLoad(transform.gameObject);
        _gameManager.MainGameUIIsLoaded();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("_GAME_MENU_SCENE");
    }

    public void getListOfFieldsCoordinates()
    {
        Transform boardTransform = GameObject.Find("Board").transform;
        List<GameObject> Children = new List<GameObject>();
        foreach (Transform child in boardTransform)
        {
            Children.Add(child.gameObject);
            Debug.Log(child.name);
        }
    }

    public void SowSeed()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        fieldMenu.SetActive(false);

        GameObject fieldNameHolder = GameObject.Find("/GameUI/FieldMenu/Panel/FieldName");
        string fieldName = fieldNameHolder.GetComponent<UnityEngine.UI.Text>().text;
        var tmp = fieldName.Split('[', ']')[1].Split(';');
        Vector3 fieldCoordinates = new Vector3(float.Parse(tmp[0]), float.Parse(tmp[1]), float.Parse(tmp[2]));

        Debug.Log(fieldCoordinates);
    }

    public void ExitFieldMenu()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        fieldMenu.SetActive(false);
    }

    public void RefreshPanels()
    {
        GameObject tmp = GameObject.Find("/GameUI");
        Debug.Log(tmp);
        Debug.Log("Refresh!");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

}


