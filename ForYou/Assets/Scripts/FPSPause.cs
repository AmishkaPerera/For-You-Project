using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class FPSPause : MonoBehaviour {

    public FirstPersonController _fps;

    // Update is called once per frame
    void Update ()
    {
        // add code to unlock mouse and lock mouse looking when paused
        if (Time.timeScale > 0f)
        {
            _fps.enabled = true;
            _fps.m_MouseLook.SetCursorLock(true);
            //m_MouseLook.SetCursorLock(true);
        }
        else
        {
            _fps.enabled = false;
            _fps.m_MouseLook.SetCursorLock(false);
            //m_MouseLook.SetCursorLock(false);
        }
        // locks mouse when unpaused with a click
        if (Time.timeScale > 0f && Input.GetButtonDown("Fire1"))
        {
            //m_MouseLook.SetCursorLock(true);
            _fps.m_MouseLook.SetCursorLock(true);
        }

	}
}


