using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    public AudioClip ac;

    // start called after awake, need to check audio here and change
    void Start()
    {
        AudioSource audsrc = GameObject.Find("Music Player").GetComponent<AudioSource>();

        print(audsrc.clip.name);
        print("clip" + ac.name);
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

