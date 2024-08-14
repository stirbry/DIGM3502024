using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

public class PrinceController : MonoBehaviour
{
    public string Name;
    //public Dialogue Dialogue;

    //the max value is 100, so 50 would mean half
    private float interestValue = 50.0f;
    public Image interestValueBar;

    public void changeInterestValue(float amt)
    {
        Debug.Log("Interest Changed by " + amt);
        interestValue += amt;
        interestValueBar.fillAmount = interestValue/100.0f;
    }    
}
