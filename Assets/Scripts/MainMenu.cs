using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;


public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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