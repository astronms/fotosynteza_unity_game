using System;
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

    public List<Vector3Int> getListOfFieldsCoordinates()
    {
        List<Vector3Int> coordinatesList = new List<Vector3Int>();
        Transform boardTransform = GameObject.Find("Board").transform;
        foreach (Transform child in boardTransform)
        {
            var tmp = child.name.Split('[', ']')[1].Split(';');
            Vector3Int fieldCoordinates = new Vector3Int(Int32.Parse(tmp[0]), Int32.Parse(tmp[1]), Int32.Parse(tmp[2]));
            coordinatesList.Add(fieldCoordinates);
        }
        return coordinatesList;
    }

    public void SowSeed()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        fieldMenu.SetActive(false);

        GameObject fieldNameHolder = GameObject.Find("/GameUI/FieldMenu/Panel/FieldName");
        string fieldName = fieldNameHolder.GetComponent<UnityEngine.UI.Text>().text;
        var tmp = fieldName.Split('[', ']')[1].Split(';');
        Vector3Int fieldCoordinates = new Vector3Int(Int32.Parse(tmp[0]), Int32.Parse(tmp[1]), Int32.Parse(tmp[2]));

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


