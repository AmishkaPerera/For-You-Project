using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public bool taken = false;
    public GameObject explosion;

	//destroys object upon collision with player
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" && (!taken) && (other.gameObject.GetComponent<CharacterController2D>().playerCanMove))
        {
            taken = true;

            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            DestroyObject(this.gameObject);

        }
    }
}
