using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 Applied to:
 The Mute Button
 */

public class ButtonVisuals : MonoBehaviour {

    public Image UnMuted;
    public Image Muted;

    private void Update()
    {
        ChangeIcon();
    }

    public void ChangeIcon()
    {
        if (AudioListener.volume == 0.0f) {
            UnMuted.enabled = false;
            Muted.enabled = true;
        }
        else if (AudioListener.volume == 1.0f) {
            Muted.enabled = false;
            UnMuted.enabled = true;
        }
    }
}
