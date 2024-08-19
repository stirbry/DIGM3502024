using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Animations;


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

    //STart shaped Buttons and dialogue
    public List<Button> optionButton;
    public List<TextMeshProUGUI> dialogueOptionFullBox;
    private bool canPlayerChoose = false;

    void Start()
    {
        gm = GetComponent<GameManager>();
        sm = GetComponent<SoundManager>();
        dialogueChart = GetComponent<Animator>();

		//hide the dialogueChoices
		foreach (Button button in optionButton)
		{
            button.gameObject.GetComponent<Image>().enabled = false;
		}
    }
    public void DisplayDialogPrince(Sprite charImg, string dialogueText, float interestChange)
    {
        sm.playDialogueChange();
        //interestChange does not concern the Prince dialogue
        header.text = "The Little Prince";
        //dialogue.text = dialogueText;
        StartCoroutine(TypeText(dialogue, dialogueText, 2.0f));

    }
    public void DisplayDialogLampLighter(Sprite charImg, string dialogueText, float interestChange)
    {
        sm.playDialogueChange();
        header.text = "The Lamplighter";
        //dialogue.text = dialogueText;
        StartCoroutine(TypeText(dialogue, dialogueText, 2.0f));

        //prince.changeInterestValue(interestChange);
    }

    public void ClearDialogueChoices()
    {
        foreach (Transform child in area)
        {
            Destroy(child.gameObject);
        }
    }
    //spawns buttons
    //Deprecated
    //we are using something else to show options
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
    //newer one for stars 
    public void DisplayNextDialogueChoicesInStars((List<string> nextDialogueTexts, List<string> nextDialogueLabels) dialogues)
	{
        canPlayerChoose = true;
       
        for (int i = 0; i < 2; i++)
		{
            //show UI first
            optionButton[i].gameObject.GetComponent<Image>().enabled = true;
            //set texts
            optionButton[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogues.nextDialogueLabels[i];
            dialogueOptionFullBox[i].text = dialogues.nextDialogueTexts[i];
            //add listener
            //Since I know there will only be 2 options, rather to hardcode it in editor
            //optionButton[i].GetComponent<Button>().onClick.AddListener(() => ChooseNextDialog(i));
            //dialogueOptionFullBox[i].GetComponentInParent<Button>().onClick.AddListener(() => ChooseNextDialog(i));
        }
    }
    public void ChooseNextDialog(int choiceIndex)
    {
        if (!canPlayerChoose) return;
        dialogueChart.SetInteger("Choice", choiceIndex);
        canPlayerChoose = false;
        //hide the dialogueChoices
        foreach (Button button in optionButton)
        {
            button.gameObject.GetComponent<Image>().enabled = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    public void onHold()
    {
        dialogueChart.SetInteger("Choice", -1);
    }

    //for printy effect
    private IEnumerator TypeText(TextMeshProUGUI dialogueTextUI, string textToDisplay, float totalDuration)
    {
        dialogueTextUI.text = ""; //clr

        int totalCharacters = textToDisplay.Length;
        float timePerCharacter = totalDuration / totalCharacters;

        for (int i = 0; i < totalCharacters; i++)
        {
            dialogueTextUI.text += textToDisplay[i]; // Append the next character
            yield return new WaitForSeconds(timePerCharacter); // Wait before displaying the next character
        }
    }
    private System.Collections.IEnumerator ScaleTween(RectTransform target, Vector3 startScale, Vector3 endScale, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale;

        onComplete?.Invoke();
    }
}
