﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    private Vector2 window;

    private void Awake()
    {
        AudioManager.PlayMusic("cave");  
    }
    void Start()
    {
        window = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

}
