using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 Applied to:
 Arbitrary Name GameObject
 */

public class LoadLevel : MonoBehaviour {

    public void playTheGame()
        {
            SceneManager.LoadScene(1);
            
        }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}