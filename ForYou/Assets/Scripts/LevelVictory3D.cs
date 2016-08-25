using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class LevelVictory3D : MonoBehaviour {

    public GameObject explosion;
    public FirstPersonController PlayerObj;

    //verifies if collision has occured
    bool _collided = false;

    // checks if player has collided with door or not
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

    void Update()
    {
        // check if player is at door to complete level
        if (_collided)
        {
            if (Input.GetKeyDown("e"))
            {
                print("IN");

            }
            // if explosion prefab is provide, then instantiate it
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

        }
    }

}
