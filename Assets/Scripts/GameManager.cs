using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public DayNightStatus dayNightStatus;
    //public Fire fire;

    public float waitTime = 15.0f;
    public float timer = 0.0f;
    public GameObject flame;

    // Update is called once per frame
    void Update()
    {
        switch (dayNightStatus)
        {
        case DayNightStatus.Night:
            //if(!fire.isLit) OnLose();
            break;
        case DayNightStatus.Day:
            //if(fire.isLit) OnLose();
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
        if (dayNightCycle.currentDayNightStatus == DayNightStatus.Sunset)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                Destroy(flame);
            }
        }
    }

    //Could be rattling gameObject or as simple as a UI notification
    void notifyToPutOffFire()
    {

    }
}