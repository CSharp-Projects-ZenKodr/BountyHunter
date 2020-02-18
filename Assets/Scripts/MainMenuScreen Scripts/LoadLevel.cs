using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 Applied to:
    LevelLoader
 */




public class LoadLevel : MonoBehaviour {

    public Image fade;
    private bool fade_now, LoadLevelNow;

    private void Update()
    {
        if (fade_now && fade.color.a <= 1)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + 0.05f);
            if (fade.color.a >= 1.0f)
            {
                LoadLevelNow = true; 
                Play();
            }
        }
    }

    public void Play() 
    {
        if (LoadLevelNow)
        {
            SceneManager.LoadScene(1);
        }
        fade.gameObject.SetActive(true);
        fade_now = true;
        
         
    } 
    public void Quit() { Application.Quit(); }
}