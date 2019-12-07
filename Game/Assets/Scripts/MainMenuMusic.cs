﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private Vector2 window;

    private void Awake() {
        AudioManager.StopAllMusic();
        AudioManager.PlayMusic("mainMenu");
    }
    void Start() {
        window = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

}