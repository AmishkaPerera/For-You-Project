using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer mp = null;

    public static MusicPlayer Instance
    {
        get { return mp; }
    }

    void Awake()
    { 
        

        if (mp != null && mp != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            mp = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //need to follow camera
    void Update()
    {
        transform.position = Camera.main.transform.position;

    }
}
