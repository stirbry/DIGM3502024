using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    //public DayNightStatus dayNightStatus;
    public Fire fire;

    // sets new script and empty object
    public LampNotif lampNotif;
    public GameObject lampObject;

    // prince interest
    public Image princeInterestBar;
    public float princeInterest = 100f, interestBarDepleteValPerSec = 0f;

    public float waitTime = 15.0f;
    public float timer = 0.0f;

    //ending
    public Image fadeImage;
    public TextMeshProUGUI endingHeader, endingTagline; 

    public SpriteRenderer wowBubble;

    void Start()
    {
        if (lampObject != null)
        {
            lampNotif = lampObject.GetComponent<LampNotif>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (dayNightCycle.currentDayNightStatus)
        {
        case DayNightStatus.Night:
            if(!fire.isLit) OnLose();
            //stopNotify();
            wowBubble.enabled = false;
            break;
        case DayNightStatus.Day:
            if(fire.isLit) OnLose();
            //stopNotify();
            break;
        case DayNightStatus.Sunrise:
            if(fire.isLit) notifyToPutOffFire();
            break;
        case DayNightStatus.Sunset:
            if(!fire.isLit) notifyToLightFire();
            wowBubble.enabled = true;
            break;
        default:
            break;
        }

        // Lamp Light Timer
        if (dayNightCycle.currentDayNightStatus == DayNightStatus.Sunset)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
                {
                    if (!fire.isLit)
                    {
                        timer = 0.0f;
                    }
                }
        }
        else if (dayNightCycle.currentDayNightStatus == DayNightStatus.Sunrise)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
                {
                    if (!fire.isLit)
                    {
                        timer = 0.0f;
                    }
                }
        }
    }

    void OnLose()
    {
        princeInterest -= Time.deltaTime * interestBarDepleteValPerSec;
        if(princeInterest < 0) 
        {
            princeInterest = 0f;

            GetComponent<Animator>().SetBool("LostInterest", true);
        }
        princeInterestBar.fillAmount = princeInterest / 100f;
    }

    void OnWin()
    {

    }

    //Could be rattling gameObject or as simple as a UI notification
    void notifyToLightFire()
    {
        lampNotif.StartRattle();
    }

    //Could be rattling gameObject or as simple as a UI notification
    void notifyToPutOffFire()
    {
        lampNotif.StartRattle();
    }

    void stopNotify()
    {
        lampNotif.StopRattle();
    }

    public void startEndingFade(float fadeTimer)
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeTheImage(fadeTimer));
    }
    public void startEndingTexts(string endingHeaderText, string endingTaglineText, float fadeDelayInSeconds, float fadeTimer)
    {
        endingHeader.gameObject.SetActive(true);
        endingTagline.gameObject.SetActive(true);
        StartCoroutine(StartDelay( endingHeaderText, endingTaglineText,  fadeDelayInSeconds,  fadeTimer));
    }

    private IEnumerator FadeTheImage(float fadeTimer)
    {
        float timeSoFar = 0f;
        while (timeSoFar < fadeTimer)
        {   
            timeSoFar += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, timeSoFar/fadeTimer);
            yield return null; // Wait till next frame
        }
        //make sure it stays at 1 alpha
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
    }
    private IEnumerator StartDelay(string endingHeaderText, string endingTaglineText, float fadeDelayInSeconds, float fadeTimer)
    {
        yield return new WaitForSeconds(fadeDelayInSeconds);
        endingHeader.text = endingHeaderText;
        endingTagline.text = endingTaglineText;
        StartCoroutine(FadeTheTexts(fadeTimer));
    }

    private IEnumerator FadeTheTexts(float fadeTimer)
    {
        float timeSoFar = 0f;
        endingHeader.overrideColorTags = true;
        endingTagline.overrideColorTags = true;
        while (timeSoFar < fadeTimer)
        {   
            timeSoFar += Time.deltaTime;
            endingHeader.color = new Color(1f, 1f, 1f, (timeSoFar/fadeTimer));
            endingTagline.color = new Color(1f, 1f, 1f, (timeSoFar/fadeTimer));
            yield return null; // Wait till next frame
        }
        //make sure it stays at 1 alpha
        endingHeader.color = new Color(1f, 1f, 1f, 1f);
        endingTagline.color = new Color(1f, 1f, 1f, 1f);

    }
}
