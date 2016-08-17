using UnityEngine;
using System.Collections;

public class LevelVictory : MonoBehaviour {

    public GameObject explosion;
    public CharacterController2D PlayerObj;

    // private variable to detect collision
    bool _collided = false;

    // need to check if player is in item collision area
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && (other.gameObject.GetComponent<CharacterController2D>().playerCanMove))
        {
            _collided = true;    
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Player") && (other.gameObject.GetComponent<CharacterController2D>().playerCanMove))
        {
            _collided = false;
        }

    }

    void Update()
    {
        // check if player is at victory item
        if (_collided)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                PlayerObj.Victory();
                
            }
            // if explosion prefab is provide, then instantiate it
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

        }
    }
}
