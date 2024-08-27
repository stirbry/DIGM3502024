using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger_EndingTexts : StateMachineBehaviour
{
    GameManager gm;
    public string endingHeaderText, endingTaglineText; 
    public float fadeDelayInSeconds, fadeTimer;

 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.startEndingTexts(endingHeaderText, endingTaglineText, fadeDelayInSeconds, fadeTimer);
    }

}
