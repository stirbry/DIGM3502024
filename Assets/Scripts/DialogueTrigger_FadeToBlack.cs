using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger_FadeToBlack : StateMachineBehaviour
{
    GameManager gm;
    public float fadeTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.startEndingFade(fadeTimer);
    }
}
