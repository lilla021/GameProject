﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void SceneLoader (int SceneIndex) {
   
        SceneManager.LoadScene(SceneIndex);
        }

    public void Quit()
    {
        Application.Quit();
    }
}
