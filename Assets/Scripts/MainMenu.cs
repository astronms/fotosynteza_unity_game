﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("_MAIN_SCENE");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
