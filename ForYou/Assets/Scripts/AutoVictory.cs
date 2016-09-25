using UnityEngine;
using System.Collections;

public class AutoVictory : MonoBehaviour {

    //2d collision script for auto initiate victory with player
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterController2D>().Victory();
        }
    }
}
