using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;


public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    private ColorBlock _defaultColorBlockNick;

    void Start()
    {

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _defaultColorBlockNick = GameObject.Find("/Menu/NewGameMenu/Player_1/NickInput").GetComponent<TMPro.TMP_InputField>().colors;
    }

    void OnGUI()
    {
        for (int i = 0; i < 4; i++)
        {
            TMPro.TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType").GetComponent<TMPro.TMP_Dropdown>();
            TMPro.TMP_InputField nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput").GetComponent<TMPro.TMP_InputField>();
            
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


    public async void NewGame()
    {
        string[] playersNicks = new string[4];
        int numberOfPlayers = 0;
        await LoadMainGameScene(); //Load main game scene and get MainGameUI object.. Don't touch!

        for (int i = 0; i < 4; i++)
        {
            TMPro.TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType").GetComponent<TMPro.TMP_Dropdown>();
            if (dropdown.options[dropdown.value].text == "Brak")
                break; 

            TMPro.TMP_InputField nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput").GetComponent<TMPro.TMP_InputField>();
            if (nick.text.Length == 0)
                return; //Tutaj dodaj Error message na UI

            playersNicks[numberOfPlayers] = nick.text;
            numberOfPlayers++;
        }

        _gameManager.startNewGame(numberOfPlayers, playersNicks);
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("_MAIN_SCENE");
    }

    public void OpenTutorial()
    {
        System.Diagnostics.Process.Start("https://portalgames.blob.core.windows.net/fotosynteza/Fotosynteza-Instrukcja.pdf");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private async Task LoadMainGameScene() //please don't touch this...
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("_MAIN_SCENE");

        while (true)
        {
            if (async.progress >= 0.9f)
                break;
        }

    }
}