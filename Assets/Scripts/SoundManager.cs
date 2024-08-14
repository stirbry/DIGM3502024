using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip dialogueChangeCue;
    AudioSource speaker;
    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playDialogueChange()
    {
        speaker.clip = dialogueChangeCue;
        speaker.Play();
    }
}
