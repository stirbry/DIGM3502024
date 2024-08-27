using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public bool isLit, isPickedUp, lamplightEquip, snufferEquip;
    public SpriteRenderer fireSprite, lampOn, lampOff;

    private Vector2 originalPos;
    private RectTransform toolImage;

    public RectTransform snufferImage, lighterImage; // two images, the lighter and snuffer respectively
    public Button button1, button2; // buttons, 1 is for snuffer, 2 is lighter

    public AudioClip lighter, extinguish;

    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
        lamplightEquip = false;
        snufferEquip = false;
        lampOn.enabled = false;
        lampOff.enabled = true;
    }

    void Update(){
        if (isPickedUp && toolImage != null){
            MoveImageWithMouse();

            if(Input.GetMouseButtonDown(1) && isLamplightEquipped()){
                PutDownImage(button2);
                lamplightEquip = false;
            }
            else if(Input.GetMouseButtonDown(1) && isSnufferEquipped()){
                PutDownImage(button1);
                snufferEquip = false;
            }
        }

        
    }
    // image behavior

    private void PickUpImage(RectTransform imageToPick, Button button){
        toolImage = imageToPick;
        originalPos = toolImage.anchoredPosition;
        isPickedUp = true;
        button.interactable = false;
    }

    private void PutDownImage(Button button){
        toolImage.anchoredPosition = originalPos;
        toolImage = null;
        isPickedUp = false;
        button.interactable = true;
    }

    private void MoveImageWithMouse(){
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            toolImage.parent as RectTransform,
            Input.mousePosition,
            null,
            out mousePos
        );
        toolImage.anchoredPosition = mousePos;
    }

    // fire behavior

    public void fireUpdate()
    {
        if((isSnufferEquipped() && isFireOn())) // turn off fire
        { 
            switchFireStatus();
            fireSprite.enabled = false;
            lampOn.enabled = false;
            lampOff.enabled = true;
            
            AudioSource.PlayClipAtPoint(extinguish, toolImage.position);

            // for images
            PutDownImage(button1);
            snufferEquip = false;
        }
        else if (isLamplightEquipped() && (isFireOn() ^ isLamplightEquipped())) // turn on fire
        {
            switchFireStatus();
            fireSprite.enabled = true;
            lampOn.enabled = true;
            lampOff.enabled = false;

             AudioSource.PlayClipAtPoint(lighter, toolImage.position);

            // for images
            PutDownImage(button2);
            lamplightEquip = false;
        }
    }

    public void equipSnuffer()
    {
        snufferEquip = !snufferEquip;
        lamplightEquip = false;
        if (isPickedUp){
            PutDownImage(button2);
        }
        PickUpImage(snufferImage, button1);
        
    }

    public void equipLamplight()
    {
        lamplightEquip = !lamplightEquip;
        snufferEquip = false;
        if (isPickedUp){
            PutDownImage(button1);
        }
        PickUpImage(lighterImage, button2);
        
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
        GameObject.Find("GameManager").GetComponent<TutorialBehaviour>().AdvanceTutorialStage();
    }

}