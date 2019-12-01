using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    void Awake()
    {
        sentences = new Queue<string>();

    }
    public void StartDialog(Dialog dialog)
    {
        Debug.Log("test dialog" + dialog.dialog);

        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    void EndDialog()
    {
        Debug.Log("End convo");
    }
}
  
