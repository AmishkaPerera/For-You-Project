using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class LevelText3D : MonoBehaviour {

    public GameObject txt;

    bool _collided = false;


    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (other.gameObject.GetComponent<FirstPersonController>().enabled = true))
        {
            _collided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player") && (other.gameObject.GetComponent<FirstPersonController>().enabled = true))
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