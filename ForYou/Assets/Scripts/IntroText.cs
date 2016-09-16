using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {

    public GameObject txt;

    bool _collided = false;


    void OnTriggerEnter(Collider other)
    {
        print("collideddd");
        if ((other.tag == "MainCamera"))
        {
            _collided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.tag == "MainCamera"))
        {
            _collided = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_collided && Time.timeScale > 0)
        {
            txt.SetActive(true);
        }
        else
        {
            txt.SetActive(false);
        }
    }
}
