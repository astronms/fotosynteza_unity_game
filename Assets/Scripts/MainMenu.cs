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
        if (this.ToString() == "NewGameMenu (MainMenu)")
        {
            try
            {
                _defaultColorBlockNick = FindObjectOfType<TMPro.TMP_InputField>().colors;
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Not loaded _defaultColorBlockNick - "+ ex);
            }
        }
    }

    /// <summary>
    /// called when updating the GUI
    /// </summary>
    void OnGUI()
    {
        if (this.ToString() == "NewGameMenu (MainMenu)")
        {
            for (int i = 0; i < 4; i++)
            {
                TMPro.TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType")
                    .GetComponent<TMPro.TMP_Dropdown>();
                TMPro.TMP_InputField nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput")
                    .GetComponent<TMPro.TMP_InputField>();

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
    }


    public void NewGame()
    {
        if(MainGameUI.Instance != null)
        {
            GameObject GameUI = GameObject.Find("GameUI");
            Destroy(GameUI);
        }
        SceneManager.LoadSceneAsync("_MAIN_SCENE");
        int playerNumber = 0;
        int numberForNick = 1;
        List<Player> players = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            TMPro.TMP_Dropdown dropdown = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/PlayerType")
                .GetComponent<TMPro.TMP_Dropdown>();
            TMPro.TMP_InputField nick = GameObject.Find("/Menu/NewGameMenu/Player_" + (i + 1) + "/NickInput")
                .GetComponent<TMPro.TMP_InputField>();
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
}

