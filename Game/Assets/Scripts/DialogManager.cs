using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public float letterPause = 0.1f;
    public int actionCount = 0;
    
    public bool isTutorial;
    public bool isForest;
    public bool isCave;

    Animator mAnimator;

    //public AudioClip textSound;
    //public AudioClip clickSound;

    GameObject dialogPanel;
    GameObject player;

    private Queue<string> sentences = new Queue<string>();

    void Awake()
    {
        dialogPanel = GameObject.FindWithTag("DialogPanel");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Animator>().enabled = false;
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
  
    public void DisplayNextSentence()
    {
        player.GetComponent<Animator>().enabled = false;
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeText(sentence));
    }

    IEnumerator TypeText(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            
            dialogText.text += letter;
            // if (typeSound1 && typeSound2)
            //    SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
    void EndDialog()
    {
        dialogPanel.GetComponent<Animator>().SetBool("disappear", true);
        player.GetComponent<Animator>().enabled = true;
    }
}
  
