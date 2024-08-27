using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogues : StateMachineBehaviour
{
    public int thisTutorialStageIndex;
    public bool isThisStart = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isThisStart) GameObject.Find("GameManager").GetComponent<TutorialBehaviour>().StartGame();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("GameManager").GetComponent<TutorialBehaviour>().enableGameObjects(thisTutorialStageIndex);
    }

}
