using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Animations;
public enum CharacterType
{
    LittlePrince,
    LampLighter
}
public class DialogState : StateMachineBehaviour
{
    Animator thisDialogSystem;
    public Sprite characterSprite;     //might need sprites with expressions
    public CharacterType character;
    [TextArea] public string dialogText; //text box for dialogues (works with long)
    public float intrestValue = 0.0f;
    public int outgoingTransitionsCount = 0; //var to store the number of outgoing transitions
    DislogueController dialogueController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        thisDialogSystem = animator;
        dialogueController = GameObject.Find("GameManager").GetComponent<DislogueController>();

        //clear dialogue choices
        dialogueController.ClearDialogueChoices();

        //show the new dialogues fromn the text field
        if(character == CharacterType.LittlePrince)
        {
            dialogueController.DisplayDialogPrince(characterSprite, dialogText, intrestValue);
        }
        else
        {
            dialogueController.DisplayDialogLampLighter(characterSprite, dialogText, intrestValue);
        }

        //now we check
        switch (outgoingTransitionsCount)
        {
            case -1:
                //-1 is used if we need to hold the dialogue
                break;
            case 0:
                //0 means no loner more dialogue, end game 
                break;
            case 1:
                //this means only 1 dialogue, so it means it is dialogue exchange
                break;
            default:
                //so if there is multiple choices to speak, it will need to display next dialog choices, and also 
                dialogueController.onHold();
                dialogueController.DisplayNextDialogueChoicesInStars(nextChoicesDialogues(animator, stateInfo, layerIndex));
                break;
        }
        
    }

    public void SetChoice(int indexOfNextDialog)
    {
        if(indexOfNextDialog < outgoingTransitionsCount)
        {
            thisDialogSystem.SetInteger("Choice", indexOfNextDialog);
        }
    }

    //tuple needed
    private (List<string> nextDialogueTexts, List<string> nextDialogueLabels) 
        nextChoicesDialogues(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<string> nextDialogueTexts = new List<string>();
        List<string> nextDialogueLabels = new List<string>();
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        if (animatorController != null)
        {
            //get the stateMachine from the casted animationcontroller
            AnimatorStateMachine stateMachine = animatorController.layers[layerIndex].stateMachine;

            //look up the current state with hash
            foreach (ChildAnimatorState childState in stateMachine.states)
            {
                if (childState.state.nameHash == stateInfo.shortNameHash)
                {
                    AnimatorState currentState = childState.state;

                    //loop through AnimatorStateTransition
                    //I could not have a public AnimatorStateTransition and set it manually, so we have to do this long ass process
                    foreach (AnimatorStateTransition transition in currentState.transitions)
                    {
                        // destination state of the transition
                        AnimatorState destinationState = transition.destinationState;

                        //DialogState from the destination state
                        DialogState dialogState = destinationState.behaviours.OfType<DialogState>().FirstOrDefault();

                        //check if the destination state has a DialogState 
                        if (dialogState != null)
                        {
                            nextDialogueTexts.Add(dialogState.dialogText);
                            nextDialogueLabels.Add(destinationState.name);
                        }
                    }
                    break;
                }
            }
        }
        return (nextDialogueTexts, nextDialogueLabels);
    }
}
