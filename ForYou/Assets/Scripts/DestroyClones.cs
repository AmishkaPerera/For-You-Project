using UnityEngine;
using System.Collections;

public class DestroyClones : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        if (Application.loadedLevelName == "Final Level" || Application.loadedLevelName == "Intro Level")
        {
            print("FIONAL");
            var clones = GameObject.FindGameObjectsWithTag("Music Player");
            foreach (var c in clones)
            {
                Destroy(c);
            }
        }
    }
}
