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

    public void Play() { SceneManager.LoadScene(1); } 
    public void Quit() { Application.Quit(); }
}