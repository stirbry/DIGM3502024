using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Asset")]
public class Dialogue : ScriptableObject // ScriptableObject means this is an object that stores data managed through Unity
{
    // First node of the conversation
    public DialogueNode RootNode;
}
