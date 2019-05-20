using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 Applied to:
 The Mute Button
 */

public class ButtonVisuals : MonoBehaviour {

    public Image soundOn;
    public Image muted;
    public GameController controller;

    private void Update()
    {
        swapIcons();
    }

    public void swapIcons()
    {
        if (AudioListener.volume == 0.0f)
        {
            soundOn.enabled = false;
            muted.enabled = true;
        }
        else if (AudioListener.volume == 1.0f)
        {
            muted.enabled = false;
            soundOn.enabled = true;
        }
    }
}
