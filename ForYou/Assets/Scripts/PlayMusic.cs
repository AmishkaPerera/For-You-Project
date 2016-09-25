using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    public AudioClip ac;

    // start called after awake, need to check audio here and change
    void Start()
    {
        AudioSource audsrc = GameObject.Find("Music Player").GetComponent<AudioSource>();

        if (ac != null)
        {
            if (audsrc.clip.name != ac.name)
            {
                audsrc.clip = ac;
                audsrc.Play();
            }
        }
    }


}

