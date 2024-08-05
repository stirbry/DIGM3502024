using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Code by: Dul Thomas on YouTube
// https://www.youtube.com/watch?v=dcPIuTS_usM

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {  get; private set; }

    // UI references
    public GameObject DialogueParent; // Main container for dialogue UI
    public TextMeshProUGUI DialogTitleText, DialogBodyText; // Text components for title and body
    public GameObject responseButtonPrefab; // Prefab for generating response buttons
    public Transform responseButtonContainer; // Container to hold response buttons
    public PrinceController theLittlePrince; //Prince char to keep updating interest with each option

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initially hide the dialogue UI
        HideDialogue();
    }

    // Starts the dialogue with given title and dialogue node
    public void StartDialogue(string title, DialogueNode node)
    {
        // Display the dialogue UI
        ShowDialogue();

        // Set dialogue title and body text
        DialogTitleText.text = title;
        DialogBodyText.text = node.dialogueText;



        // Remove any existing response buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create and setup response buttons based on current dialogue node
        foreach (DialogueResponse response in node.responses)
        {
            GameObject buttonObj = Instantiate(responseButtonPrefab);
            buttonObj.transform.SetParent(responseButtonContainer);

            RectTransform thisButtonTransform =  buttonObj.GetComponent<RectTransform>();
            thisButtonTransform.anchoredPosition = new Vector2(0.0f,0.0f);
            thisButtonTransform.localScale = Vector3.one;
            thisButtonTransform.localRotation = Quaternion.identity;


            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            // Setup button to trigger SelectResponse when clicked
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response, title));
        }
    }

    // Handles response selection and triggers next dialogue node
    public void SelectResponse(DialogueResponse response, string title)
    {
        //set Prince's interest Value after selecting a response
        theLittlePrince.changeInterestValue(response.responseValue);

        // Check if there's a follow-up node
        if (!response.nextNode.IsLastNode())
        {
            StartDialogue(title, response.nextNode); // Start next dialogue
        }
        else
        {
            // If no follow-up node, end the dialogue
            HideDialogue();
        }
    }

    // Hide the dialogue UI
    public void HideDialogue()
    {
        DialogueParent.SetActive(false);
    }

    // Show the dialogue UI
    private void ShowDialogue()
    {
        DialogueParent.SetActive(true);
    }

    // Check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }
}
