using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public int _currentPlayerId; //refers to the player who is currently taking his turn

    // zbiór pól
    public List<Field> _fields = new List<Field>();
    private MainGameUI _mainGameUI;

    //players 
    public List<Player> _players = new List<Player>();

    public int _round;

    public Dictionary<string, int> PendingLoad = new Dictionary<string, int>();

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    ///     method called when the main game starts
    /// </summary>
    /// <param name="players">List of active players</param>
    public void startNewGame(List<Player> players)
    {
        _players = players;
        _fields = new List<Field>();
        _currentPlayerId = 0;
        _round = 1;
        //Please do all other operations once MainGameUI object will be loaded, ie. in MainGameUIIsLoaded method. 
    }

    public void MainGameUIIsLoaded() //This method is called when MainGameUI object has been created. It should be called only once. 
    {
        _mainGameUI = MainGameUI.Instance;

        //Removing all existing trees on UI
        UITree.Instance.removeAllTrees();
        //Generating _fields structure 
        List<FieldVector> _fieldsCoordinates = _mainGameUI.GetListOfFieldsCoordinates();
        foreach (FieldVector coordinate in _fieldsCoordinates)
        {
            Field field = new Field();
            field._vector = coordinate;
            _fields.Add(field);
        }

    }

    public void FazePhotosynthesis(int position)
    {
        foreach (Field field in _fields)
        {
            int x = field._vector.x;
            int y = field._vector.y;
            int z = field._vector.z;
            int pointoflightstoadd = 0;

            // weryfikacja czy na polu ustawione jest drzewo
            if (field._assignment != null)
                if (field._assignment._treeLevel != TreeObject.TreeLvl.SEED)
                {
                    // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                    pointoflightstoadd = SetTreeValue(field);

                    switch (position)
                    {
                        case 0:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                x--;
                                y++;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        case 1:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                x--;
                                z++;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        case 2:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                y--;
                                z++;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        case 3:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                x++;
                                y--;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        case 4:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                x++;
                                z--;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        case 5:
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 1; distance < 4; distance++)
                            {
                                y++;
                                z--;
                                // weryfikacja czy pole istnieje
                                if (FieldRangeVerification(x, y, z)) break;
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);
                            }

                            break;

                        default:
                            Console.WriteLine("Error - point of lights not counted.");
                            break;
                    }

                    // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                    field._assignment._player.ChangePointOfLights(pointoflightstoadd);
                }
        }
    }

    // weryfikacja zakresu pola
    private bool FieldRangeVerification(int x, int y, int z)
    {
        return x < 0 || y < 0 || z < 0 || x > 6 || y > 6 || z > 6;
    }

    // weryfikacja możliwych do uzyskania punktów światła dla danego drzewa na względem danego dystansu
    private int FieldVerification(int x, int y, int z, int pointoflightstoadd, int distance, Field field)
    {
        var result = _fields.Where(f => f._vector.x == x && f._vector.y == y && f._vector.z == z).FirstOrDefault();

        if (result._assignment != null)
            if (result._assignment._treeLevel != TreeObject.TreeLvl.SEED)
                switch (field._assignment._treeLevel)
                {
                    case TreeObject.TreeLvl.SMALL:
                        switch (distance)
                        {
                            case 1:
                                // niezależnie jakie drzewo jest na pozycji zawsze zacieni małe drzewo 
                                pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                break;
                            case 2:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                break;
                            case 3:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                break;
                            default:
                                Console.WriteLine("Error - point of lights - shading - smalltree.");
                                break;
                        }

                        break;
                    case TreeObject.TreeLvl.MID:
                        switch (distance)
                        {
                            case 1:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                break;
                            case 2:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                else if (result._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                break;
                            case 3:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                break;
                            default:
                                Console.WriteLine("Error - point of lights - shading - midtree.");
                                break;
                        }

                        break;
                    case TreeObject.TreeLvl.BIG:
                        switch (distance)
                        {
                            case 1:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                else if (result._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                break;
                            case 2:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 3);
                                else if (result._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                break;
                            case 3:
                                if (result._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                else
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 3);
                                break;
                            default:
                                Console.WriteLine("Error - point of lights - shading - bigtree.");
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine("Error - point of lights to add not verified.");
                        break;
                }

        return pointoflightstoadd;
    }

    // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa 
    private int SetTreeValue(Field field)
    {
        int pointoflightstoadd = 0;

        switch (field._assignment._treeLevel)
        {
            case TreeObject.TreeLvl.SMALL:
                pointoflightstoadd = 1;
                break;
            case TreeObject.TreeLvl.MID:
                pointoflightstoadd = 2;
                break;
            case TreeObject.TreeLvl.BIG:
                pointoflightstoadd = 3;
                break;
            default:
                Console.WriteLine("Error - point of lights - shading - tree.");
                break;
        }

        return pointoflightstoadd;
    }

    private Field GetFieldByVector(FieldVector vector)
    {
        foreach (Field field in _fields)
            if (field._vector.Equals(vector))
                return field;
        return null;
    }

    private int GetFieldLevel(Field field)
    {
        int[] array = new int[3] { Math.Abs((field._vector.x) - 3), Math.Abs((field._vector.y) - 3), Math.Abs((field._vector.z) - 3) };
        int max = array.Max();
        int value = 0;

        switch (max)
        {
            case 0:
                value = 4;
                break;

            case 1:
                value = 3;
                break;

            case 2:
                value = 2;
                break;

            case 3:
                value = 1;
                break;
        }
        return value;
    }


    public actionType AvailableActionOnField(FieldVector vector)
    {
        Field field = GetFieldByVector(vector);
        if (field._already_used)
            return actionType.none;

        if (field._assignment == null) //SEED
        {
            if (_round == 1)
            {
                if (_players[_currentPlayerId].NumberOfSmallTrees > 2 && GetFieldLevel(field) == 1)
                    return actionType.plant;
            }
            else
            {
                if (_players[_currentPlayerId].NumberOfSeeds > 0 && _players[_currentPlayerId].PointOfLights > 0 && field._is_active[_currentPlayerId] == true)
                    return actionType.seed;
            }

            return actionType.none;
        }

        if (field._assignment._player == _players[_currentPlayerId] &&
            field._assignment._treeLevel < TreeObject.TreeLvl.BIG) //Upgrafe
        {
            if (field._assignment._treeLevel == TreeObject.TreeLvl.SEED &&
                _players[_currentPlayerId].NumberOfSmallTrees > 0 && _players[_currentPlayerId].PointOfLights > 0)
                return actionType.upgrade;
            if (field._assignment._treeLevel == TreeObject.TreeLvl.SMALL &&
                _players[_currentPlayerId].NumberOfMediumTrees > 0 && _players[_currentPlayerId].PointOfLights > 1)
                return actionType.upgrade;
            if (field._assignment._treeLevel == TreeObject.TreeLvl.MID &&
                _players[_currentPlayerId].NumberOfLargeTrees > 0 && _players[_currentPlayerId].PointOfLights > 2)
                return actionType.upgrade;
            return actionType.none;
        }

        if (field._assignment._player == _players[_currentPlayerId] &&
            field._assignment._treeLevel == TreeObject.TreeLvl.BIG &&
            _players[_currentPlayerId].PointOfLights > 4) //cut
            return actionType.cut;
        return actionType.none;
    }

    public bool AddSeed(FieldVector coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        if (field._assignment != null || field._is_active[_currentPlayerId] == false)
            return false;

        TreeObject seed = new TreeObject(TreeObject.TreeLvl.SEED, _players[_currentPlayerId]);
        field._assignment = seed;
        field._already_used = true;
        _players[_currentPlayerId].ChangeNumberOfSeeds(-1);
        _players[_currentPlayerId].ChangePointOfLights(-1);
        return true;
    }

    public bool PlantTree(FieldVector coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        if (field._assignment != null)
            return false;

        TreeObject tree = new TreeObject(TreeObject.TreeLvl.SMALL, _players[_currentPlayerId]);
        field._assignment = tree;
        _players[_currentPlayerId].ChangeNumberOfSmallTrees(-1);
        field._already_used = true;
        SetNeighborhoodToActive(field);
        return true;
    }

    public TreeObject.TreeLvl UpgradeTree(FieldVector coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        field._assignment.LvlUp();
        field._already_used = true;

        switch (field._assignment._treeLevel)
        {
            case TreeObject.TreeLvl.SMALL:
                _players[_currentPlayerId].ChangeNumberOfSmallTrees(-1);
                _players[_currentPlayerId].ChangePointOfLights(-1);
                break;
            case TreeObject.TreeLvl.MID:
                _players[_currentPlayerId].ChangeNumberOfMediumTrees(-1);
                _players[_currentPlayerId].ChangePointOfLights(-2);
                break;
            case TreeObject.TreeLvl.BIG:
                _players[_currentPlayerId].ChangeNumberOfLargeTrees(-1);
                _players[_currentPlayerId].ChangePointOfLights(-3);
                break;
        }
        SetNeighborhoodToActive(field);
        return field._assignment._treeLevel;
    }

    public bool CutTree(FieldVector coordinates)
    {
        Field field = GetFieldByVector(coordinates);
        SetNeighborhoodToInactive(field);
        if (_players[_currentPlayerId].ChangePointOfLights(-4))
        {
            _players[_currentPlayerId].ChangePoints(GetFieldLevel(field));
            field._assignment = null;
            field._already_used = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextRound()
    {
        FazePhotosynthesis(Sun_Rotation.Instance.sun_position);
        Sun_Rotation.Instance.Next_Sun_Position();
        _round++;
        if (_round == 2)
        {
            FinalRound();
        }
        foreach (var field in _fields)
            field._already_used = false;
    }

    private void FinalRound()
    {
        AddPointsFromUnusedPointsOfLights();
        var winner = GetWinner(_players);
        ShowResult(winner);
    }

    private void ShowResult(string winner)
    {
        MessageBox.Show(
            "Wygrał " + winner,
            "Gratulacje",
            result => { SceneManager.LoadScene("_START_MENU_SCENE"); }
        );
    }

    private void AddPointsFromUnusedPointsOfLights()
    {
        foreach (var player in _players) player.ChangePoints(player.PointOfLights % 3);
    }

    private string GetWinner(List<Player> players)
    {
        int maxPoints = players.OrderByDescending(p => p.Points).First().Points;
        List<Player> winners = players.Where(p => p.Points == maxPoints).ToList();
        int winnersCount = winners.Count();
        if (winnersCount == 1)
        {
            SaveDataForRanking(winners.OrderByDescending(p => p.Points).First());
            return winners.OrderByDescending(p => p.Points).First().Nick;
        }
        AddAdditionalPoints(winners);
        if (winners.OrderByDescending(p => p.Points).Count(p => p.Points == winners.OrderByDescending(w => w.Points).First().Points) > 1)
            return ResultStringForPlayersTie(winners);
        SaveDataForRanking(winners.OrderByDescending(p => p.Points).First());
        return winners.OrderByDescending(p => p.Points).First().Nick;
    }

    private static void SaveDataForRanking(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        Ranking ranking = new Ranking();
        if (File.Exists(Application.persistentDataPath + "/ranking.bin"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/ranking.bin", FileMode.Open);
            ranking = (Ranking) bf.Deserialize(file);
            file.Close();
            File.Delete(Application.persistentDataPath + "/ranking.bin");
            FileStream fileFinal = File.Create(Application.persistentDataPath + "/ranking.bin");
            BinaryFormatter bf1 = new BinaryFormatter();
            ranking.winnersList.Add(player);
            bf1.Serialize(fileFinal, ranking);
            fileFinal.Close();
        }
        else
        {
            FileStream file = File.Create(Application.persistentDataPath + "/ranking.bin");
            ranking.winnersList.Add(player);
            bf.Serialize(file, ranking);
            file.Close();
        }
       
    }

    private void AddAdditionalPoints(List<Player> winners)
    {
        foreach (var winner in winners)
            foreach (var field in _fields)
                if (field._assignment != null)
                {
                    if (winner == field._assignment._player)
                        winner.ChangePoints(1);
                }

    }

    private static string ResultStringForPlayersTie(List<Player> winners)
    {
        var result = winners.OrderByDescending(p => p.Points)
            .Where(p => p.Points == winners.OrderByDescending(w => w.Points).First().Points);
        string resultString = String.Empty;
        foreach (var winner in result)
        {
            SaveDataForRanking(winner);
            resultString += " i " + winner.Nick;
        }
        return resultString;
    }

    public void EndPlayerTurn()
    {
        PlayerMadeMove(false);
        if (_currentPlayerId + 1 < _players.Count)
        {
            _currentPlayerId++;
        }
        else
        {
            NextRound();
            _currentPlayerId = 0;
        }
    }

    public void PlayerMadeMove(bool move = true)
    {
        _players[_currentPlayerId].MadeMove = move;
    }

    public bool CheckIfPlayerMadeMove()
    {
        return _players[_currentPlayerId].MadeMove;
    }

    private IEnumerable<Field> NeighborhoodFields(Field _field)
    {
        return _fields.Where(field =>
            field._vector.x >= MinVal(_field._vector.x - (int)_field._assignment._treeLevel) &&
            field._vector.x <= MaxVal(_field._vector.x + (int)_field._assignment._treeLevel) &&
            field._vector.y >= MinVal(_field._vector.y - (int)_field._assignment._treeLevel) &&
            field._vector.y <= MaxVal(_field._vector.y + (int)_field._assignment._treeLevel) &&
            field._vector.z >= MinVal(_field._vector.z - (int)_field._assignment._treeLevel) &&
            field._vector.z <= MaxVal(_field._vector.z + (int)_field._assignment._treeLevel));
    }

    private void SetNeighborhoodToActive(Field _field)
    {
        var neighborhood = NeighborhoodFields(_field);
        foreach (Field field_ in neighborhood) SetFieldActive(field_);
    }

    private void SetNeighborhoodToInactive(Field _field)
    {
        var neighborhood = NeighborhoodFields(_field);
        foreach (Field field_ in neighborhood) SetFieldInactive(field_);
    }

    private void SetFieldActive(Field field)
    {
        field._is_active[_currentPlayerId] = true;
    }

    private void SetFieldInactive(Field field)
    {
        field._is_active[_currentPlayerId] = false;
    }

    private int MinVal(int x)
    {
        return x >= 0 ? x : 0;
    }

    private int MaxVal(int x)
    {
        return x <= 6 ? x : 6;
    }
    public Save CreateSaveGameObject()
    {
        Save save = new Save();
        foreach (var player in _players)
        {
            save.activePlayers.Add(player);
        }

        foreach (var field in _fields)
        {
            save.activeFields.Add(field);
        }

        save.round = _round;
        save.activePlayerId = _currentPlayerId;
        save.SunRotation = Sun_Rotation.Instance.sun_position;
        return save;
    }

    private void ClearGame()
    {
        _players.Clear();
        _fields.Clear();
        _currentPlayerId = 0;
        _round = 1;

    }
    public void LoadSaveGameObject(Save save)
    {
        ClearGame();
        foreach (var savePlayer in save.activePlayers)
        {
            _players.Add(savePlayer);
        }

        foreach (var saveField in save.activeFields)
        {
            _fields.Add(saveField);
        }

        if (Sun_Rotation.Instance == null)
            PendingLoad.Add("sun_position", save.SunRotation);
        else
            Sun_Rotation.Instance.sun_position = save.SunRotation;

        _round = save.round;
        _currentPlayerId = save.activePlayerId;
    }

}
