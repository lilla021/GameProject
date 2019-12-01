using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogPanelController : MonoBehaviour
{

    Text moveInstructions;
    Text jumpInstructions;
    Text dreamWorldInstructions;
    Text inGameMenuInstructions;
    Text mainMenuInstructions;

    public bool moveInst;
    public bool jumpInst;
    public bool dreamInst;
    public bool inGameMenuInst;
    public bool mainMenuInst;
    void Start()
    {
        Text[] allDialogTexts = GetComponents<Text>();
        moveInst = allDialogTexts[0];
        jumpInst = allDialogTexts[1];
        dreamInst = allDialogTexts[2];
        inGameMenuInst = allDialogTexts[3];
        mainMenuInst = allDialogTexts[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInst) { }; 
        if (jumpInst) { }; 
        if (dreamInst) { }; 
        if (inGameMenuInst) { }; 
        if (mainMenuInst) { }; 
    }
}
