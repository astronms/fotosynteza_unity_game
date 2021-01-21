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

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// called when updating the GUI
    /// </summary>
    void OnGUI()
    {
        if (this.ToString() == "NewGameMenu (MainMenu)")
        {
            ColorBlock defaultColorBlockNick = GameObject.Find("/Menu/NewGameMenu/Player_1/NickInput").GetComponent<TMPro.TMP_InputField>().colors;
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
                    nick.colors = defaultColorBlockNick;
                }
            }
        }
    }


    public async void NewGame()
    {
        string[] playersNicks = new string[4];
        int playerNumber = 0;
        await LoadMainGameScene(); //Load main game scene and get MainGameUI object.. Don't touch!
        List<Player> players = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            TMPro.TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType").GetComponent<TMPro.TMP_Dropdown>();
            TMPro.TMP_InputField nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput").GetComponent<TMPro.TMP_InputField>();
            if (dropdown.options[dropdown.value].text != "Brak")
            {
                if (nick.text.Length == 0)
                {
                    nick.text = "player_" + i;
                }
                players.Add(new Player(playerNumber, nick.text, 0));
                playerNumber++;
            }
        }
        _gameManager.startNewGame(players);
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