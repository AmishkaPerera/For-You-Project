using UnityEngine;
using System.Collections;

public class DestroyClones : MonoBehaviour {

	// specialized script to remove default music player for cutscene scenes
	void Awake()
    {
        if (Application.loadedLevelName == "Final Level" || Application.loadedLevelName == "Intro Level")
        {
            var clones = GameObject.FindGameObjectsWithTag("Music Player");
            foreach (var c in clones)
            {
                Destroy(c);
            }
        }
    }
}
