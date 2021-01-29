using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    private GameManager _gameManager;
    private GameObject _treesGroup;
    public Text ltrees_count;
    public Text mtrees_count;

    [Header("UI variables")] public Text name1_ui;

    public Text name2_ui;
    public Text name3_ui;
    public Text name4_ui;
    public Text player1_ui_points;
    public Text player2_ui_points;
    public Text player3_ui_points;
    public Text player4_ui_points;

    public Text playername_ui;

    public Text round_count;
    public Text seeds_count;
    public Text strees_count;
    public Text sun_points;
    public static MainGameUI Instance { get; private set; }

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DontDestroyOnLoad(transform.gameObject);
        _gameManager.MainGameUIIsLoaded();
        _treesGroup = GameObject.Find("Trees");
        refreshGameBoardTrees();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("_GAME_MENU_SCENE");
    }

    public List<FieldVector> GetListOfFieldsCoordinates()
    {
        List<FieldVector> coordinatesList = new List<FieldVector>();
        Transform boardTransform = GameObject.Find("Board").transform;
        foreach (Transform child in boardTransform)
        {
            var tmp = child.name.Split('[', ']')[1].Split(';');
            FieldVector fieldCoordinates = Field.GetCoordinates(tmp);
            coordinatesList.Add(fieldCoordinates);
        }

        return coordinatesList;
    }
    private void NewSeedInit(GameObject tmps, GameObject clickedField, string fieldName)
    {
        GameObject newSeed = Instantiate(tmps, clickedField.transform.position, Quaternion.identity);
        newSeed.transform.parent = _treesGroup.transform;
        newSeed.transform.name = "Tree_" + fieldName.Split('_')[1];
    }

    public void SowSeed()
    {
        var fieldName = GetFieldName(out var fieldCoordinates);

        bool result = _gameManager.AddSeed(fieldCoordinates);

        if (result)
        {
            GameObject clickedField = GameObject.Find("/Board/" + fieldName);
            GameObject tmps = Resources.Load("Seed_Player_" + _gameManager._currentPlayerId) as GameObject;
            NewSeedInit(tmps, clickedField, fieldName);
            _gameManager.PlayerMadeMove();
        }
        else
        {
            MessageBox.Show("Błąd podczas siania nasiona.","Błąd");
            Debug.Log("Error during sowing seed.");
        }
    }

    public void PlantTree() //this method shoud be called only in first game round
    {
        var fieldName = GetFieldName(out var fieldCoordinates);
        bool result = _gameManager.PlantTree(fieldCoordinates);

        if (result)
        {
            GameObject clickedField = GameObject.Find("/Board/" + fieldName);
            GameObject tmps = Resources.Load("SmallTree_Player_" + _gameManager._currentPlayerId) as GameObject;
            NewSeedInit(tmps, clickedField, fieldName);
            _gameManager.PlayerMadeMove();
        }
        else
        {
            MessageBox.Show("Błąd podczas siania nasiona.", "Błąd");
            Debug.Log("Error during sowing seed.");
        }
    }

    private static string GetFieldName(out FieldVector fieldCoordinates)
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        fieldMenu.SetActive(false);

        GameObject fieldNameHolder = GameObject.Find("/GameUI/FieldMenu/Panel/FieldName");
        string fieldName = fieldNameHolder.GetComponent<Text>().text;
        var tmp = fieldName.Split('[', ']')[1].Split(';');
        fieldCoordinates = Field.GetCoordinates(tmp);
        return fieldName;
    }

    private static void DestroyExistingTree(string fieldName)
    {
        GameObject existingTree = GameObject.Find("/Trees/Tree_" + fieldName.Split('_')[1]);
        Destroy(existingTree);
    }

    public void UpgradeTree()
    {
        string fieldName = GetFieldName(out var fieldCoordinates);
        DestroyExistingTree(fieldName);
        TreeObject.TreeLvl lvl = _gameManager.UpgradeTree(fieldCoordinates);
        GameObject clickedField = GameObject.Find("/Board/" + fieldName);
        GameObject tree;
        GameObject tmps;
        switch (lvl)
        {
            case TreeObject.TreeLvl.SMALL:
                tmps = Resources.Load("SmallTree_Player_" + _gameManager._currentPlayerId) as GameObject;
                tree = Instantiate(tmps, clickedField.transform.position, Quaternion.identity);
                tree.transform.parent = _treesGroup.transform;
                tree.transform.name = "Tree_" + fieldName.Split('_')[1];
                break;
            case TreeObject.TreeLvl.MID:
                tmps = Resources.Load("MediumTree_Player_" + _gameManager._currentPlayerId) as GameObject;
                tree = Instantiate(tmps, clickedField.transform.position, Quaternion.identity);
                tree.transform.parent = _treesGroup.transform;
                tree.transform.name = "Tree_" + fieldName.Split('_')[1];
                break;
            case TreeObject.TreeLvl.BIG:
                tmps = Resources.Load("BigTree_Player_" + _gameManager._currentPlayerId) as GameObject;
                tree = Instantiate(tmps, clickedField.transform.position, Quaternion.identity);
                tree.transform.parent = _treesGroup.transform;
                tree.transform.name = "Tree_" + fieldName.Split('_')[1];
                break;
        }
        _gameManager.PlayerMadeMove();
    }



    public void CutTree()
    {
        var fieldName = GetFieldName(out var fieldCoordinates);
        if ( _gameManager.CutTree(fieldCoordinates))
        {
            DestroyExistingTree(fieldName);
            _gameManager.PlayerMadeMove();
        }
    }

    public void ExitFieldMenu()
    {
        GameObject fieldMenu = GameObject.Find("/GameUI/FieldMenu/Panel");
        fieldMenu.SetActive(false);
    }

    public void EndPlayerTurn()
    {
        ExitFieldMenu();
        if (!_gameManager.CheckIfPlayerMadeMove())
        {
            MessageBox.Show
            (
                "Nie wykonałeś żadnego ruchu. \nCzy chcesz zakończyć swoją rundę?",
                "Uwaga",
                (result) =>
                {
                    if (result.ToString() == "Yes")
                    {
                        _gameManager.EndPlayerTurn();
                    }
                },
                MessageBoxButtons.YesNo
            );
        }
        else
            _gameManager.EndPlayerTurn();
    }

    public void BuySeed()
    {
        Player player = _gameManager._players[_gameManager._currentPlayerId];
        if (player.ChangePointOfLights(-1))
        {
            player.ChangeNumberOfSeeds(1);
            _gameManager.PlayerMadeMove();
        }
    }

    public void BuySmallTree()
    {
        Player player = _gameManager._players[_gameManager._currentPlayerId];
        if (player.ChangePointOfLights(-2))
        {
            player.ChangeNumberOfSmallTrees(1);
            _gameManager.PlayerMadeMove();
        }
    }

    public void BuyMediumTree()
    {
        Player player = _gameManager._players[_gameManager._currentPlayerId];
        if (player.ChangePointOfLights(-3))
        {
            player.ChangeNumberOfMediumTrees(1);
            _gameManager.PlayerMadeMove();
        }
    }

    public void BuyBigTree()
    {
        Player player = _gameManager._players[_gameManager._currentPlayerId];
        if (player.ChangePointOfLights(-4))
        {
            player.ChangeNumberOfLargeTrees(1);
            _gameManager.PlayerMadeMove();
        }
    }

    public void Update() //variables for UI handling. method runs constantly 1/frame, refrashing variable values
    {
        //general UI. visible for all players all the time. score board- player names, points, round number

        name1_ui.text = _gameManager._players[0].Nick;
        name2_ui.text = _gameManager._players[1].Nick;
        player1_ui_points.text = _gameManager._players[0].Points + " pkt";
        player2_ui_points.text = _gameManager._players[1].Points + " pkt";
        if (_gameManager._players.Count > 2)
        {
            name3_ui.text = _gameManager._players[2].Nick;
            player3_ui_points.text = _gameManager._players[2].Points + " pkt";
        }

        if (_gameManager._players.Count > 3)
        {
            name4_ui.text = _gameManager._players[3].Nick;
            player4_ui_points.text = _gameManager._players[3].Points + " pkt";
        }

        round_count.text = "Runda #" + _gameManager._round;

        //individual UI. _currentPlayerId needed for if's to work. case for player 0 working only.

        int currentPlayer = _gameManager._currentPlayerId;
        playername_ui.text = _gameManager._players[currentPlayer].Nick;
        sun_points.text = _gameManager._players[currentPlayer].PointOfLights.ToString();
        seeds_count.text = _gameManager._players[currentPlayer].NumberOfSeeds.ToString();
        strees_count.text = _gameManager._players[currentPlayer].NumberOfSmallTrees.ToString();
        mtrees_count.text = _gameManager._players[currentPlayer].NumberOfMediumTrees.ToString();
        ltrees_count.text = _gameManager._players[currentPlayer].NumberOfLargeTrees.ToString();
    }

    public void refreshGameBoardTrees()
    { 
        foreach(var field in _gameManager._fields)
        {
            if (field._assignment != null)
            {
                int idOwner = field._assignment._player.Id;
                string fieldNameCoord = "[" + field._vector.x + ";" + field._vector.y + ";" + field._vector.z + "]";
                GameObject visualField = GameObject.Find("Board/Field_" + fieldNameCoord);
                GameObject tree;
                GameObject tmps;
                switch (field._assignment._treeLevel)
                {
                    case TreeObject.TreeLvl.SEED:
                        tmps = Resources.Load("Seed_Player_" + idOwner) as GameObject;
                        tree = Instantiate(tmps, visualField.transform.position, Quaternion.identity);
                        tree.transform.parent = _treesGroup.transform;
                        tree.transform.name = "Tree_" + fieldNameCoord;
                        break;
                    case TreeObject.TreeLvl.SMALL:
                        tmps = Resources.Load("SmallTree_Player_" + idOwner) as GameObject;
                        tree = Instantiate(tmps, visualField.transform.position, Quaternion.identity);
                        tree.transform.parent = _treesGroup.transform;
                        tree.transform.name = "Tree_" + fieldNameCoord;
                        break;
                    case TreeObject.TreeLvl.MID:
                        tmps = Resources.Load("MediumTree_Player_" + idOwner) as GameObject;
                        tree = Instantiate(tmps, visualField.transform.position, Quaternion.identity);
                        tree.transform.parent = _treesGroup.transform;
                        tree.transform.name = "Tree_" + fieldNameCoord;
                        break;
                    case TreeObject.TreeLvl.BIG:
                        tmps = Resources.Load("BigTree_Player_" + idOwner) as GameObject;
                        tree = Instantiate(tmps, visualField.transform.position, Quaternion.identity);
                        tree.transform.parent = _treesGroup.transform;
                        tree.transform.name = "Tree_" + fieldNameCoord;
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}