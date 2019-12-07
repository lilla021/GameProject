using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamFilterScript : MonoBehaviour
{
    public GameObject DreamFilter;
    public GameObject StatsMenu;
    public GameObject QuitMenu;
    public GameObject DialogPanel;
    
    void Update()
    {
        DreamFilter.SetActive(PlayerData.IsInDream);

        if (Input.GetKeyDown("m"))
        {
            Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
            StatsMenu.SetActive(!StatsMenu.activeSelf);
        }
        if(Input.GetKeyDown("escape")) {
            Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
            QuitMenu.SetActive(!QuitMenu.activeSelf);
            DialogPanel.SetActive(false);
            DialogPanel.SetActive(true);
            // DialogPanel.transform.localScale = new Vector3(Time.timeScale, Time.timeScale, Time.timeScale);
            // DialogPanel.SetActive(!DialogPanel.activeSelf);
        }
    }
}
