using UnityEngine;
using System.Collections;

public class FinalLevelVictory : MonoBehaviour {

    public Fader fdr;
    public string levelAfterComplete;

	void OnTriggerEnter(Collider other)
    {
        print("collided");

        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(10);
        
        float fadeTime = fdr.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(levelAfterComplete);


    }
}
