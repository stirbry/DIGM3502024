using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

[System.Serializable]
public class DialogueNode
{
    public string dialogueText;
    public List<DialogueResponse> responses;

    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}
