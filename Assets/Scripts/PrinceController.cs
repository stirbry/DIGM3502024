using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

public class PrinceController : MonoBehaviour
{
    public string Name;
    public Dialogue Dialogue;

    private void Update()
    {
        // Initiates dialogue when the Space Bar is pressed
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpeakTo();
        }
    }

    // Trigger dialogue for the Prince
    public void SpeakTo()
    {
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
    }
}
