using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

[System.Serializable]
public class DialogueResponse
{
    public string responseText; // Response option to be chosen
    public int responseValue; //numerical value to show how good or bad this response is
    public DialogueNode nextNode; // Text that appears after option is chosen
 
}
