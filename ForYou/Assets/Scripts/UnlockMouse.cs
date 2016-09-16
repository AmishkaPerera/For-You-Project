using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class UnlockMouse : MonoBehaviour {

    public FirstPersonController fps; 
    
    void Awake()
    {
        fps.m_MouseLook.SetCursorLock(false);
    }
	
}
