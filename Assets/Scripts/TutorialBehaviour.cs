using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public List<GameObject> objectsToEnableAfter1, objectsToEnableAfter2, objectsToEnableAfter3, objectsToEnableAfter4;
    private List<GameObject>[] objectsToEnableAfter = new List<GameObject>[4];
    public Animator anim;
    public float interestBarDepleteMult = 3.2f;

    public SpriteRenderer startFade;

    //
    private int tutorialStageNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        objectsToEnableAfter[0] = objectsToEnableAfter1;
        objectsToEnableAfter[1] = objectsToEnableAfter2;
        objectsToEnableAfter[2] = objectsToEnableAfter3;
        objectsToEnableAfter[3] = objectsToEnableAfter4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(FadeTheIntro());
    }

    public void AdvanceTutorialStage()
    {
        if(tutorialStageNum > 4) return;
        tutorialStageNum++;
        anim.SetInteger("TutorialIndex", tutorialStageNum);
    }

    public void enableGameObjects(int index)
    {
        foreach(GameObject obj in objectsToEnableAfter[index])
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator FadeTheIntro()
    {
        float timeSoFar = 0f;
        while (timeSoFar < 2f)
        {   
            timeSoFar += Time.deltaTime;
            startFade.color = new Color(0f, 0f, 0f, 1f -(timeSoFar/2f));
            yield return null;
        }
        GetComponent<GameManager>().interestBarDepleteValPerSec = interestBarDepleteMult;
        anim.SetBool("Start", true);
        startFade.gameObject.SetActive(false);
    }
}
