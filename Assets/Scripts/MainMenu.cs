using System;
using System.Collections.Generic;
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
            SetButtonInteractable("Menu/LoadSaveMenu/LoadButton", true);
        dropdown.ClearOptions();
        dropdown.AddOptions(saves);
    }

    public void LoadGameSave()
    {
        string save = GetSelectedGameSave();
        if(save == "Brak")
            MessageBox.Show("Brak dostępnego zapisu do wczytania.", "Uwaga");
        else
        {
            Debug.Log("Test");
            //TODO: Define action 
        }
    }

    public void SaveGameStatus()
    {
        string gameName = GetGameSaveName();
        if (gameName == "Brak" || gameName == "Nazwa..." || gameName.Length == 0)
            MessageBox.Show("Nieprawidłowa nazwa zapisu.", "Uwaga");
        else
        {
            //TODO: Define action
        }
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("_MAIN_SCENE");
    }

    public void OpenTutorial()
    {
        Application.OpenURL("https://portalgames.blob.core.windows.net/fotosynteza/Fotosynteza-Instrukcja.pdf");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private List<string> GetAvailableGameSaves()
    {
        return new List<string> {"Zapis 1", "Zapis 2"}; //TODO: define this method to gather game saves
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