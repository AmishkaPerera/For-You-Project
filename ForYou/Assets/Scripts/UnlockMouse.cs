using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class UnlockMouse : MonoBehaviour {

    // using mouselook form class
    public FirstPersonController fps; 
    
    // forces mouse to be unlocked upon loading of certain levels
    void Awake()
    {
        fps.m_MouseLook.SetCursorLock(false);
    }
	
}
