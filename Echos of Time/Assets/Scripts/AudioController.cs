using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioClip[] audioClips;
    public bool stateSound = true;

    // Start is called before the first frame update
    void Start()
    {
        //AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        //audioSource.clip = clip;
        //audioSource.loop = true;
        //audioSource.Play();
    }

    public void MuteSound()
    {
        stateSound = !stateSound;
        audioSource.enabled = stateSound;
    }

    public void VolumeMusical(float value)
    {
        audioSource.volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
