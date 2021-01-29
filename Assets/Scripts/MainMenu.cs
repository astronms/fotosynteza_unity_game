using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private ColorBlock _defaultColorBlockNick;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (ToString() == "NewGameMenu (MainMenu)")
            try
            {
                _defaultColorBlockNick = FindObjectOfType<TMP_InputField>().colors;
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Not loaded _defaultColorBlockNick - " + ex);
            }
    }

    /// <summary>
    ///     called when updating the GUI
    /// </summary>
    private void OnGUI()
    {
        if (ToString() == "NewGameMenu (MainMenu)")
            for (int i = 0; i < 4; i++)
            {
                var dropdown = GetDropdownOption(i, out var nick);

                if (dropdown.options[dropdown.value].text == "Brak")
                {
                    nick.pointSize = 0.0f;
                    nick.readOnly = true;
                    nick.colors = new ColorBlock();
                }
                else
                {
                    nick.pointSize = 40.0f;
                    nick.readOnly = false;
                    nick.colors = _defaultColorBlockNick;
                }
            }
    }

    public void NewGame()
    {
        DestroyInstances();

        SceneManager.LoadSceneAsync("_MAIN_SCENE");
        int playerNumber = 0;
        int numberForNick = 1;
        List<Player> players = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            var dropdown = GetDropdownOption(i, out var nick);
            if (dropdown.options[dropdown.value].text != "Brak")
            {
                if (nick.text.Length == 0)
                {
                    nick.text = "player " + numberForNick;
                    numberForNick++;
                }

                players.Add(new Player(playerNumber, nick.text, 0));
                playerNumber++;
            }
        }

        _gameManager.startNewGame(players);
    }

    private static void DestroyInstances()
    {
        if (MainGameUI.Instance != null)
        {
            GameObject GameUI = GameObject.Find("GameUI");
            Destroy(GameUI);
        }

        if (Sun_Rotation.Instance != null)
        {
            GameObject sun = GameObject.Find("sun");
            Destroy(sun);
        }
    }

    public void LoadGameSavesToDropdown()
    {
        List<string> saves = GetAvailableGameSaves();
        TMP_Dropdown dropdown = GameObject.Find("/Menu/LoadSaveMenu/Dropdown")
            .GetComponent<TMP_Dropdown>();
        if (saves.Count == 0)
        {
            saves.Add("Brak");
            SetButtonInteractable("Menu/LoadSaveMenu/LoadButton", false);
        }
        else
        {
            SetButtonInteractable("Menu/LoadSaveMenu/LoadButton", true);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(saves);
    }

    public void LoadGameSave()
    {
        string save = GetSelectedGameSave();
        if (save == "Brak")
        {
            MessageBox.Show("Brak dostępnego zapisu do wczytania.", "Uwaga");
        }
        else
        {
            DestroyInstances();

            SceneManager.LoadSceneAsync("_MAIN_SCENE");
            LoadGame(save);
        }
    }

    public void SaveGameStatus()
    {
        string gameName = GetGameSaveName();
        if (gameName == "Brak" || gameName == "Nazwa..." || gameName.Length == 0)
            MessageBox.Show("Nieprawidłowa nazwa zapisu.", "Uwaga");
        else
            SaveGame(gameName);
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("_MAIN_SCENE");
    }

    public void OpenRanking()
    {
        if (File.Exists(Application.persistentDataPath + "/ranking.bin"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ranking.bin", FileMode.Open);
            Ranking ranking = new Ranking();
            ranking = (Ranking) bf.Deserialize(file);
            string winnersString = string.Empty;
            int i = 1;
            foreach (Player winner in ranking.winnersList.OrderByDescending(w => w.Points))
            {
                winnersString += "Miejsce " + i + ": " + winner.Nick + " - " + winner.Points + "pkt \n";
                i++;
            }

            MessageBox.Show(winnersString);
        }
        else
        {
            MessageBox.Show(" Zagraj w grę!", "Ranking jeszcze nie istnieje");
        }
    }

    public void OpenTutorial()
    {
        Application.OpenURL("https://portalgames.blob.core.windows.net/fotosynteza/Fotosynteza-Instrukcja.pdf");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SaveGame(string fileName)
    {
        Save save = _gameManager.CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + ".save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    private void LoadGame(string fileName)
    {
        string savePath = Application.persistentDataPath + "/" + fileName + ".save";
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            Save save = (Save) bf.Deserialize(file);
            file.Close();
            _gameManager.LoadSaveGameObject(save);
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    private List<string> GetAvailableGameSaves()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("*.save");
        List<string> stringFiles = new List<string>();
        foreach (var file in files) stringFiles.Add(file.Name.Replace(".save", ""));
        return stringFiles;
    }

    private string GetSelectedGameSave()
    {
        TMP_Dropdown dropdown = GameObject.Find("Menu/LoadSaveMenu/Dropdown")
            .GetComponent<TMP_Dropdown>();
        return dropdown.options[dropdown.value].text;
    }

    private string GetGameSaveName()
    {
        TMP_InputField gameSaveName = GameObject.Find("/Menu/SaveMenu/SaveNameInput")
            .GetComponent<TMP_InputField>();
        return gameSaveName.text;
    }

    private void SetButtonInteractable(string path, bool interactable)
    {
        Button button = GameObject.Find(path)
            .GetComponent<Button>();
        button.interactable = interactable;
    }

    private static TMP_Dropdown GetDropdownOption(int i, out TMP_InputField nick)
    {
        TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType")
            .GetComponent<TMP_Dropdown>();
        nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput")
            .GetComponent<TMP_InputField>();
        return dropdown;
    }
}