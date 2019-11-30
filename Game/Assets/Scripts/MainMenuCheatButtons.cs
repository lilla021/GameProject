using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCheatButtons : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CheatButtons;
       

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            CheatButtons.SetActive(!CheatButtons.activeSelf);
        }
    }
}
