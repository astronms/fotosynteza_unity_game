using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private MainGameUI _mainGameUI;

    // zbiór pól
    List<Field> _fields = new List<Field>();
    // tablica wyszukiwania pól 
    Field[,,] _fieldsarray = new Field[6, 6, 6];
    // wartość pozycji słońca
    /*int _sunposition;*/
    Sun_Rotation sun_Rotation;
    public int _round = 1;

    //players 
    public List<Player> _players = new List<Player>();
    public int _currentPlayerId; //refers to the player who is currently taking his turn

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    /// method called when the main game starts
    /// </summary>
    /// <param name="players">List of active players</param>
    public void startNewGame(List<Player> players)
    {
        _players = players;
        _currentPlayerId = 0;
        //Please do all other operations once MainGameUI object will be loaded, ie. in MainGameUIIsLoaded method. 
    }

    public void MainGameUIIsLoaded() //This method is called when MainGameUI object has been created. It should be called only once. 
    {
        _mainGameUI = MainGameUI.Instance;

        //Generating _fields structure 
        List<Vector3Int> fieldsCoordinates = _mainGameUI.getListOfFieldsCoordinates();
        foreach (Vector3Int coordinate in fieldsCoordinates)
        {
            Field field = new Field();
            field._vector = coordinate;
            _fields.Add(field);
        }

        //Getting sun_Rotation object
        sun_Rotation = GameObject.Find("sun").GetComponent<Sun_Rotation>();
    }

    public void FazePhotosynthesis(int position)
    {
        switch (position)
        {

            case 1:
                // x-1; y+1; z;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null) // 
                    {
                        if (field._assignment._treeLevel != TreeObject.TreeLvl.SEED)
                        {
                            // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                            pointoflightstoadd = SetTreeValue(field);
                            // sprawdzanie odległości do dystansu 3 pól przed drzewem
                            for (int distance = 0; distance < 3; distance++)
                            {
                                x--;
                                y++;
                                // weryfikacja czy pole istnieje
                                if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                                {
                                    break;
                                }
                                else
                                {
                                    // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                    pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                                }
                            }

                            // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                            field._assignment._player.PointOfLights += pointoflightstoadd;
                        }
                    }

                }
                break;

            case 2:
                // x-1; y; z+1;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null || field._assignment._treeLevel != TreeObject.TreeLvl.SEED) // 
                    {
                        // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                        pointoflightstoadd = SetTreeValue(field);
                        // sprawdzanie odległości do dystansu 3 pól przed drzewem
                        for (int distance = 0; distance < 3; distance++)
                        {
                            x--;
                            z++;
                            // weryfikacja czy pole istnieje
                            if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                            {
                                break;
                            }
                            else
                            {
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                            }

                        }

                        // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                        field._assignment._player.PointOfLights += pointoflightstoadd;

                    }

                }
                break;


            case 3:
                // x; y-1; z+1;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null || field._assignment._treeLevel != TreeObject.TreeLvl.SEED) // 
                    {
                        // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                        pointoflightstoadd = SetTreeValue(field);
                        // sprawdzanie odległości do dystansu 3 pól przed drzewem
                        for (int distance = 0; distance < 3; distance++)
                        {
                            y--;
                            z++;
                            // weryfikacja czy pole istnieje
                            if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                            {
                                break;
                            }
                            else
                            {
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                            }

                        }

                        // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                        field._assignment._player.PointOfLights += pointoflightstoadd;

                    }

                }
                break;


            case 4:
                // x+1; y-1; z;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null || field._assignment._treeLevel != TreeObject.TreeLvl.SEED) // 
                    {
                        // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                        pointoflightstoadd = SetTreeValue(field);
                        // sprawdzanie odległości do dystansu 3 pól przed drzewem
                        for (int distance = 0; distance < 3; distance++)
                        {
                            x++;
                            y--;
                            // weryfikacja czy pole istnieje
                            if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                            {
                                break;
                            }
                            else
                            {
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                            }

                        }

                        // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                        field._assignment._player.PointOfLights += pointoflightstoadd;
                    }

                }
                break;


            case 5:
                // x+1; y; z-1;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null || field._assignment._treeLevel != TreeObject.TreeLvl.SEED) // 
                    {
                        // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                        pointoflightstoadd = SetTreeValue(field);
                        // sprawdzanie odległości do dystansu 3 pól przed drzewem
                        for (int distance = 0; distance < 3; distance++)
                        {
                            x++;
                            z--;
                            // weryfikacja czy pole istnieje
                            if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                            {
                                break;
                            }
                            else
                            {
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                            }

                        }

                        // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                        field._assignment._player.PointOfLights += pointoflightstoadd;

                    }

                }
                break;


            case 6:
                // x; y+1; z-1;
                foreach (Field field in _fields)
                {
                    int x = field._vector.x;
                    int y = field._vector.y;
                    int z = field._vector.z;
                    int pointoflightstoadd = 0; // ustawić na poziom drzewa

                    // weryfikacja czy na polu ustawione jest drzewo
                    if (field._assignment != null || field._assignment._treeLevel != TreeObject.TreeLvl.SEED) // 
                    {
                        // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa
                        pointoflightstoadd = SetTreeValue(field);
                        // sprawdzanie odległości do dystansu 3 pól przed drzewem
                        for (int distance = 0; distance < 3; distance++)
                        {
                            y++;
                            z--;
                            // weryfikacja czy pole istnieje
                            if ((x < 0 || y < 0 || z < 0) || (x > 6 || y > 6 || z > 6))
                            {
                                break;
                            }
                            else
                            {
                                // weryfikacja czy na polu o dystansie distance przed drzewem jest inne drzewo
                                pointoflightstoadd = FieldVerification(x, y, z, pointoflightstoadd, distance, field);

                            }

                        }

                        // dodanie graczowi odpowieniej ilości punktów za dane drzewo
                        field._assignment._player.PointOfLights += pointoflightstoadd;

                    }

                }
                break;


            default:
                Console.WriteLine("Error - point of lights not counted.");
                break;


        }
    }

    // weryfikacja możliwych do uzyskania punktów światła dla danego drzewa na względem danego dystansu
    private int FieldVerification(int _x, int _y, int _z, int _pointoflightstoadd, int _distance, Field _field)
    {
        int x = _x;
        int y = _y;
        int z = _z;
        int pointoflightstoadd = _pointoflightstoadd;
        int distance = _distance;
        Field field = _field;

        if (_fieldsarray[x, y, z]._assignment != null && _fieldsarray[x, y, z]._assignment._treeLevel != TreeObject.TreeLvl.SEED)
        {
            switch (field._assignment._treeLevel)
            {
                case TreeObject.TreeLvl.SMALL:
                    {
                        switch (distance)
                        {
                            case 1:
                                {
                                    // niezależnie jakie drzewo jest na pozycji zawsze zacieni małe drzewo 
                                    pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    break;
                                }
                            case 2:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Error - point of lights - shading - smalltree.");
                                    break;
                                }
                        }
                        break;
                    }

                case TreeObject.TreeLvl.MID:
                    {
                        switch (distance)
                        {
                            case 1:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                    }
                                    else if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Error - point of lights - shading - midtree.");
                                    break;
                                }
                        }
                        break;

                    }

                case TreeObject.TreeLvl.BIG:
                    {
                        switch (distance)
                        {
                            case 1:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                    }
                                    else if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 0);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.SMALL)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 3);
                                    }
                                    else if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.MID)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 1);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (_fieldsarray[x, y, z]._assignment._treeLevel == TreeObject.TreeLvl.BIG)
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 2);
                                    }
                                    else
                                    {
                                        pointoflightstoadd = Math.Min(pointoflightstoadd, 3);
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Error - point of lights - shading - bigtree.");
                                    break;
                                }
                        }
                        break;
                    }

                default:
                    Console.WriteLine("Error - point of lights to add not verified.");
                    break;
            }

        }

        return pointoflightstoadd;
    }

    // ustawianie bazowych punktów światła do uzyskania dla sprawdzanego drzewa 
    private int SetTreeValue(Field _field)
    {
        Field field = _field;
        int pointoflightstoadd = 0;

        switch (field._assignment._treeLevel)
        {
            case TreeObject.TreeLvl.SMALL:
                {
                    pointoflightstoadd = 1;
                    break;
                }
            case TreeObject.TreeLvl.MID:
                {
                    pointoflightstoadd = 2;
                    break;
                }
            case TreeObject.TreeLvl.BIG:
                {
                    pointoflightstoadd = 3;
                    break;
                }
            default:
                {
                    Console.WriteLine("Error - point of lights - shading - tree.");
                    break;
                }

        }
        return pointoflightstoadd;
    }

    // przypisywanie listy pól do tablicy wyszukiwania pól
    public void FillFieldArray()
    {
        foreach (Field field in _fields)
        {
            _fieldsarray[field._vector.x, field._vector.y, field._vector.z] = field;
        }

    }

    private Field GetFieldByVector(Vector3Int vector)
    {
        foreach (Field field in _fields)
        {
            if (field._vector == vector)
                return field;
        }
        return null;
    }

    private int GetFieldLevel(Field field)
    {
        Vector3Int position = field._vector;
        if (position.x == 0 || position.x == 6 || position.y == 0 || position.y == 6 || position.z == 0 || position.z == 6)
            return 1;
        else
            return 2;
    }


    public actionType AvailableActionOnField(Vector3Int vector) 
    {
        Field field = GetFieldByVector(vector);
        if (field._already_used == true)
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
                if (_players[_currentPlayerId].NumberOfSeeds > 0 && _players[_currentPlayerId].PointOfLights > 0)
                    return actionType.seed;
            }
            return actionType.none;
        }
        else //UPGRADE / CUT
        {
            if (field._assignment._player == _players[_currentPlayerId] && field._assignment._treeLevel < TreeObject.TreeLvl.BIG) //Upgrafe
            {
                if (field._assignment._treeLevel == TreeObject.TreeLvl.SEED && _players[_currentPlayerId].NumberOfSmallTrees > 0 && _players[_currentPlayerId].PointOfLights > 0)
                    return actionType.upgrade;
                else if (field._assignment._treeLevel == TreeObject.TreeLvl.SMALL && _players[_currentPlayerId].NumberOfMediumTrees > 0 && _players[_currentPlayerId].PointOfLights > 1)
                    return actionType.upgrade;
                else if (field._assignment._treeLevel == TreeObject.TreeLvl.MID && _players[_currentPlayerId].NumberOfLargeTrees > 0 && _players[_currentPlayerId].PointOfLights > 2)
                    return actionType.upgrade;
                else
                    return actionType.none;
            }
            else if (field._assignment._player == _players[_currentPlayerId] && field._assignment._treeLevel == TreeObject.TreeLvl.BIG && _players[_currentPlayerId].PointOfLights > 4) //cut
                return actionType.cut;
            else
                return actionType.none;
        }
    }

    public bool AddSeed(Vector3Int coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        if (field._assignment != null)
            return false;

        TreeObject seed = new TreeObject(TreeObject.TreeLvl.SEED, _players[_currentPlayerId]);
        field._assignment = seed;
        field._already_used = true;
        _players[_currentPlayerId].NumberOfSeeds--;
        _players[_currentPlayerId].PointOfLights--;
        return true;
    }

    public bool PlantTree(Vector3Int coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        if (field._assignment != null)
            return false;

        TreeObject tree = new TreeObject(TreeObject.TreeLvl.SMALL, _players[_currentPlayerId]);
        field._assignment = tree;
        _players[_currentPlayerId].NumberOfSmallTrees--;
        field._already_used = true;
        return true;
    }

    public TreeObject.TreeLvl UpgradeTree(Vector3Int coordinates)
    {
        Field field = GetFieldByVector(coordinates);

        field._assignment.lvlUp();
        field._already_used = true;

        switch(field._assignment._treeLevel)
        {
            case TreeObject.TreeLvl.SMALL:
                _players[_currentPlayerId].NumberOfSmallTrees--;
                _players[_currentPlayerId].PointOfLights--;
                break;
            case TreeObject.TreeLvl.MID:
                _players[_currentPlayerId].NumberOfMediumTrees--;
                _players[_currentPlayerId].PointOfLights -= 2;
                break;
            case TreeObject.TreeLvl.BIG:
                _players[_currentPlayerId].NumberOfLargeTrees--;
                _players[_currentPlayerId].PointOfLights -=3;
                break;
        }
        return field._assignment._treeLevel;
    }

    public void CutTree(Vector3Int coordinates)
    {
        Field field = GetFieldByVector(coordinates);
        field._already_used = true;
        _players[_currentPlayerId].PointOfLights -= 4;
        field._assignment = null;
    }

    public void NextRound()
    {
        //FazePhotosynthesis(sun_Rotation.sun_position);
        sun_Rotation.Next_Sun_Position();
        _round++;
        foreach (var field in _fields)
            field._already_used = false;
    }

    public void EndPlayerTurn()
    {
        if ((_currentPlayerId + 1) < _players.Count)
            _currentPlayerId++;
        else
        {
            NextRound();
            _currentPlayerId = 0;
        }
    }
}

