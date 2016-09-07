using UnityEngine;
using System.Collections;

public class AutoVictory : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
    {
        print("collided");

        if (other.gameObject.tag == "Player")
        {
            print("with player");
            other.gameObject.GetComponent<CharacterController2D>().Victory();
        }
    }
}
