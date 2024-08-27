using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum DayNightStatus
    {
        Day,
        Sunset,
        Night,
        Sunrise
    }
public class DayNightCycle : MonoBehaviour
{
    private float rotationValToKeepTrack = 0.0f, rotatorAmount=0.0f; 

    public GameObject Sun, Moon;
    public Transform SunUpPos, SunDownPos, MoonUpPos, MoonDownPos;

    public Animator anim;

    public DayNightStatus currentDayNightStatus;

    void Start()
    {
        //because we start at daytime
        currentDayNightStatus = DayNightStatus.Day;
    }
    // Update is called once per frame
    void Update()
    {
        //6 means 6deg per sec
        rotatorAmount = 6.0f * Time.deltaTime;
        //Rotate and keep track of rotation value so far (same as checking EulerAngle at Z)
        transform.Rotate(0f, 0f, rotatorAmount);
        rotationValToKeepTrack += rotatorAmount;

        //reset to 0 for easier tracking of values
        if(rotationValToKeepTrack>=360) rotationValToKeepTrack-= 360;

        //Set the daynight staus to day for starters, the rest of the code can handle if it needs to change
        currentDayNightStatus = DayNightStatus.Day;
        //check for night 
        if(rotationValToKeepTrack>+135 && rotationValToKeepTrack<=225)
        {
            currentDayNightStatus = DayNightStatus.Night;
        }

        //For Sun to go Down and Moon to come up
        if(rotationValToKeepTrack>45 && rotationValToKeepTrack<135)
        {
            float lerpFactor = (rotationValToKeepTrack-45)/Mathf.Abs(135-45);
            Sun.transform.position = Vector3.Lerp(SunUpPos.position, SunDownPos.position, lerpFactor);
            Moon.transform.position = Vector3.Lerp(MoonDownPos.position, MoonUpPos.position, lerpFactor);

            //Also, this is sunset
            currentDayNightStatus = DayNightStatus.Sunset;

            anim.SetBool("Day", true);
        }
        //For moon to go down and sun to come up
        if(rotationValToKeepTrack>225 && rotationValToKeepTrack<315)
        {
            float lerpFactor = (rotationValToKeepTrack-225)/Mathf.Abs(315-225);
            Moon.transform.position = Vector3.Lerp(MoonUpPos.position, MoonDownPos.position, lerpFactor);
            Sun.transform.position = Vector3.Lerp(SunDownPos.position, SunUpPos.position, lerpFactor);

            //Also, this is sunrise
            currentDayNightStatus = DayNightStatus.Sunrise;
            anim.SetBool("Day", false);
        }
    }

    public DayNightStatus getCurrentDayNightStatus()
    {
        return currentDayNightStatus;
    }
    public float getDayNightRotationValue()
    {
        return rotationValToKeepTrack;
    }
}
