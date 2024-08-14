using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DislogueController : MonoBehaviour
{
    GameManager gm;
    SoundManager sm;
    Animator dialogueChart;
    public PrinceController prince;
    //Dialogue Exchange
    public TextMeshProUGUI header, dialogue;  

    //Buttons and new Dialogue options
    public RectTransform area;
    public GameObject buttonPrefab;

    void Start()
    {
        gm = GetComponent<GameManager>();
        sm = GetComponent<SoundManager>();
        dialogueChart = GetComponent<Animator>();
    }
    public void DisplayDialogPrince(Sprite charImg, string dialogueText, float interestChange)
    {
        sm.playDialogueChange();
        //interestChange does not concern the Prince dialogue
        header.text = "The Little Prince";
        dialogue.text = dialogueText;
    }
    public void DisplayDialogLampLighter(Sprite charImg, string dialogueText, float interestChange)
    {
        sm.playDialogueChange();
        header.text = "The Lamplighter";
        dialogue.text = dialogueText;
        prince.changeInterestValue(interestChange);
    }

    public void ClearDialogueChoices()
    {
        foreach (Transform child in area)
        {
            Destroy(child.gameObject);
        }
    }
    //spawns buttons
    public void DisplayNextDialogueChoices(List<string> nextDialogues)
    {
        int dialogueOptionsCount = nextDialogues.Count;
        if (dialogueOptionsCount == 0) return;

        //Clear any existing buttons first
        ClearDialogueChoices();

        //math for spacing between buttons, vertical of anchor
        float spacingFromBottom = 0.94f/(float)(nextDialogues.Count + 1); //0.94 is after the 0.03 padding top and bottom
        Debug.Log(spacingFromBottom);
        for (int i = 0; i < dialogueOptionsCount; i++)
        {
            //Instantiate button
            GameObject button = Instantiate(buttonPrefab, area);
            button.name = "Option_" + i;
            //Set text
            button.GetComponentInChildren<TextMeshProUGUI>().text = nextDialogues[i];

            // Calculate position, from top
            //we don to calculate horizontal, unless you have chaanged the horizontal position of anchors in the editor
            //apply
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            float posY = 1.0f-(0.03f + ((i+1)*spacingFromBottom));

            float heightOfAnchors = (buttonPrefab.GetComponent<RectTransform>().anchorMax.y - buttonPrefab.GetComponent<RectTransform>().anchorMin.y); //for height og each side from middle

            buttonTransform.anchorMin = new Vector2(buttonTransform.anchorMin.x,posY-heightOfAnchors);
            buttonTransform.anchorMax = new Vector2(buttonTransform.anchorMax.x,posY+heightOfAnchors);
            //button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

            //Add the onClick listener to the button
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(() => ChooseNextDialog(index));
        }
    }
    public void ChooseNextDialog(int choiceIndex)
    {
        dialogueChart.SetInteger("Choice", choiceIndex);
    }
    public void onHold()
    {
        dialogueChart.SetInteger("Choice", -1);
    }
}
