using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    // public vars
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.5f;

    // pirvate vars 
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1; // in = -1, out = 1

	void OnGUI ()
    {  
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha); // normalise between 0 and 1

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    // sets fade direction
    public float BeginFade (int dir)
    {
        fadeDir = dir;
        return (fadeSpeed);
    }

    // calls begin fade when lvl is loaded
    void OnLevelWasLoaded ()
    {
        alpha = 1;
        BeginFade(-1);
    }

}