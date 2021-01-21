using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    public static MainGameUI Instance { get; private set; }
    private GameManager _gameManager;

    [Header("UI variables")]
    
    public Text name1_ui;
    public Text name2_ui;
    public Text name3_ui;
    public Text name4_ui;
    public Text player1_ui_points;
    public Text player2_ui_points;
    public Text player3_ui_points;
    public Text player4_ui_points;

    //public Text round_count;

    public Text playername_ui;

    public Text seeds_count;
    public Text strees_count;
    public Text mtrees_count;
    public Text ltrees_count;

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

    public void Update() //variables for UI handling. method runs constantly 1/frame, refrashing variable values
    {
        //general UI. visible for all players all the time. score board- player names, points, round number
        name1_ui.text = "gracz 1 -" + _gameManager._players[0].Nick.ToString();
        name2_ui.text = "gracz 2 -" + _gameManager._players[1].Nick.ToString();
        name3_ui.text = "gracz 3 -" + _gameManager._players[2].Nick.ToString();
        name4_ui.text = "gracz 4 -" + _gameManager._players[3].Nick.ToString();

        player1_ui_points.text = _gameManager._players[0].Points.ToString() + " points";
        player2_ui_points.text = _gameManager._players[1].Points.ToString() + " points";
        player3_ui_points.text = _gameManager._players[2].Points.ToString() + " points";
        player4_ui_points.text = _gameManager._players[3].Points.ToString() + " points";

        //round_count.text = "Runda #" + COŚ.CO.LICZY.LICZBE.TUR.ToString();;

        //individual UI. _currentPlayerId needed for if's to work. case for player 0 working only.

        //if (_currentPlayerId == 0)
        //{
        playername_ui.text = "gracz 1 -" + _gameManager._players[0].Nick.ToString();
        seeds_count.text = _gameManager._players[0].NumberOfSeeds.ToString();
        strees_count.text = _gameManager._players[0].NumberOfSmallTrees.ToString();
        mtrees_count.text = _gameManager._players[0].NumberOfMediumTrees.ToString();
        ltrees_count.text = _gameManager._players[0].NumberOfLArgeTrees.ToString();
        //}
        /*
        if (_currentPlayerId == 1)
        {
        playername_ui.text = "gracz 1 -" + _gameManager._players[0].Nick.ToString();
        seeds_count.text = _gameManager._players[1].NumberOfSeeds.ToString();
        strees_count.text = _gameManager._players[1].NumberOfSmallTrees.ToString();
        mtrees_count.text = _gameManager._players[1].NumberOfMediumTrees.ToString();
        ltrees_count.text = _gameManager._players[1].NumberOfLArgeTrees.ToString();
        }
        if (_currentPlayerId == 2)
        {
        playername_ui.text = "gracz 1 -" + _gameManager._players[0].Nick.ToString();
        seeds_count.text = _gameManager._players[2].NumberOfSeeds.ToString();
        strees_count.text = _gameManager._players[2].NumberOfSmallTrees.ToString();
        mtrees_count.text = _gameManager._players[2].NumberOfMediumTrees.ToString();
        ltrees_count.text = _gameManager._players[2].NumberOfLArgeTrees.ToString();
        }
        if (_currentPlayerId == 3)
        {
        playername_ui.text = "gracz 1 -" + _gameManager._players[0].Nick.ToString();
        seeds_count.text = _gameManager._players[3].NumberOfSeeds.ToString();
        strees_count.text = _gameManager._players[3].NumberOfSmallTrees.ToString();
        mtrees_count.text = _gameManager._players[3].NumberOfMediumTrees.ToString();
        ltrees_count.text = _gameManager._players[3].NumberOfLArgeTrees.ToString();
        }
        */




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


