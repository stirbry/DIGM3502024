using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public DayNightStatus dayNightStatus;
    public Fire fire;

    // sets new script and empty object
    public LampNotif lampNotif;
    public GameObject lampObject;

    // prince interest
    public Image princeInterestBar;
    public float princeInterest = 100f, interestBarDepleteValPerSec = 1.667f;

    public float waitTime = 15.0f;
    public float timer = 0.0f;


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
            stopNotify();
            break;
        case DayNightStatus.Day:
            if(fire.isLit) OnLose();
            stopNotify();
            break;
        case DayNightStatus.Sunrise:
            if(fire.isLit) notifyToPutOffFire();
            break;
        case DayNightStatus.Sunset:
            if(!fire.isLit) notifyToLightFire();
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
                        Debug.Log("Ran out of time.");
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
                        Debug.Log("Ran out of time.");
                    }
                }
        }
    }

    void OnLose()
    {
        princeInterest -= Time.deltaTime * interestBarDepleteValPerSec;
        if(princeInterest < 0) princeInterest = 0f;
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
}
