using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class UnlockMouse : MonoBehaviour {

    public MouseLook ml; 
    
    void Awake()
    {
        ml.SetCursorLock(false);
    }
	
}
