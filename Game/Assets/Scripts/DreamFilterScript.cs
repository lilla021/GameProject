using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamFilterScript : MonoBehaviour
{
    public GameObject DreamFilter;
    public GameObject StatsMenu;
    public GameObject QuitMenu;
    
    void Update()
    {
        DreamFilter.SetActive(PlayerData.IsInDream);

        if (Input.GetKeyDown("m"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            StatsMenu.SetActive(!StatsMenu.activeSelf);
        }
        if(Input.GetKeyDown("escape"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            QuitMenu.SetActive(!QuitMenu.activeSelf);
        }
    }
}
