using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public DayNightStatus dayNightStatus;
    public Fire fire;

    // Lamp Timer Variables
    public float waitTime = 15.0f;
    public float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        switch (dayNightStatus)
        {
        case DayNightStatus.Night:
            if(!fire.isLit) OnLose();
            break;
        case DayNightStatus.Day:
            if(fire.isLit) OnLose();
            break;
        case DayNightStatus.Sunrise:
            notifyToPutOffFire();
            break;
        case DayNightStatus.Sunset:
            notifyToLightFire();
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

    }

    void OnWin()
    {

    }

    //Could be rattling gameObject or as simple as a UI notification
    void notifyToLightFire()
    {

    }

    //Could be rattling gameObject or as simple as a UI notification
    void notifyToPutOffFire()
    {

    }
}