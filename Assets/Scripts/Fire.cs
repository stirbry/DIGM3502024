using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public bool isLit, lamplightEquip, snufferEquip;

    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
        lamplightEquip = false;
        snufferEquip = false;
    }

    public void fireUpdate()
    {
        if((isSnufferEquipped() && isFireOn()) || (isLamplightEquipped() && (isFireOn() ^ isLamplightEquipped())))
        { 
            switchFireStatus();
            Debug.Log("Fire is On? = " + isLit);
        }
        else
        {
            Debug.Log("You have the wrong tool equipped");
        }
    }

    public void equipSnuffer()
    {
        snufferEquip = !snufferEquip;
        lamplightEquip = false;
    }

    public void equipLamplight()
    {
        lamplightEquip = !lamplightEquip;
        snufferEquip = false;
    }

    public bool isLamplightEquipped()
    {
        return lamplightEquip;
    }
    public bool isSnufferEquipped()
    {
        return snufferEquip;
    }

    public bool isFireOn(){
        return isLit;
    }

    public void switchFireStatus(){
        isLit = !isLit;
    }
}
