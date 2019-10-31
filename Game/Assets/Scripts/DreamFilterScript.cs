using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamFilterScript : MonoBehaviour
{
    public GameObject DreamFilter;
    
    void Update()
    {
        if(PlayerData.IsInDream)
        {
            DreamFilter.SetActive(true);
        }
        else
        {
            DreamFilter.SetActive(false);
;       }
    }
}
