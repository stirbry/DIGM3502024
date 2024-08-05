using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

public class PrinceController : MonoBehaviour
{
    public string Name;
    public Dialogue Dialogue;

    //the max value is 100, so 50 would mean half
    private int interestValue = 50;
    public Image interestValueBar;

    private void Update()
    {
        // Initiates dialogue when the Space Bar is pressed
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpeakTo();
            changeInterestValue(0);
        }
    }

    // Trigger dialogue for the Prince
    public void SpeakTo()
    {
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
    }

    public void changeInterestValue(int amt)
    {
        interestValue += amt;
        interestValueBar.fillAmount = (float)interestValue/100.0f;
    }    
}
