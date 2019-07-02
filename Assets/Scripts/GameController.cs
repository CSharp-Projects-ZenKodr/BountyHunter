using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
APPLIED TO:
    Game Controller
 
*/

public class GameController : MonoBehaviour
{
    //Class Variables
    public GameObject backgroundSprite;
    public GameObject playerShip;
    public GameObject hazard;
    public GameObject PauseMenu;
    public GameObject pauseButton;
    public GameObject HighScore;

    public Text GameOverText;
    public Text restartText;
    public Text quitText;
    public Text LoseText;
    public Text scoreDisplay;

    public Vector3 SpawnValues;
    
    public Button quitButton;
    public Button restartButton;

    public Image restartButtonImage;
    public Image quitButtonImage;
   
    [HideInInspector] public static bool GameOverFlag = false;
    [HideInInspector] public static bool PauseFlag = false;

    private int asteroidShootScore = 10;
    private int Score = 0;
    private float winningScore = Mathf.Infinity;
    private Quaternion spawnRotation;
    private Vector3 spawnPositon;
    private WaitForSeconds asteroidWait;
    private WaitForSeconds backgroundWait;
    private GameObject[] asteroids = new GameObject[20];
    private bool Muted = false;
    


    private void Awake()
    {
        Time.timeScale = 1f;
        //PlayerPrefs.SetInt("High Score", Score);
              
    }

    private void Start()
    {
        PauseFlag = false;
        gameObject.GetComponent<AudioSource>().Play();
        AsteroidPool();
    }

    private void Update()
    {
        HighScore.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString();

        scoreDisplay.text = "Score: " + System.Convert.ToString(Score);
        if (Score >= winningScore)
        {
            GameOverFlag = true;
            Mover.flag = true; //static GameOver Flag in the Mover script. 
            
        }

        if (GameOverFlag == true)
        {
            Invoke("GameOver", 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (PauseFlag)
            {
                case true:
                    resumeGame();
                    break;
                case false:
                    pauseGame();
                    break;
            }
        }

        
    }

    void AsteroidPool()
    {
        Vector3 spawn = new Vector3(60f, -2f, -1.5f);
        for (int i = 0; i < 20; i++)
        {
            asteroids[i] = Instantiate(hazard, spawn, Quaternion.identity);
            asteroids[i].GetComponent<Mover>().enabled = false;
        }
        StartCoroutine(SpawnWaves());
        
    }

    private IEnumerator GenerateBackground()
    {
        backgroundWait = new WaitForSeconds(5);
        yield return backgroundWait;

    }

    IEnumerator SpawnWaves()
    {
        foreach (GameObject asteroid in asteroids)
        {
            if(!GameOverFlag)
            {
                asteroidWait = new WaitForSeconds(Random.Range(0.3f, 1.75f));
                spawnPositon = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                spawnRotation = Quaternion.identity;

                asteroid.transform.position = spawnPositon;
                asteroid.transform.rotation = spawnRotation;
                asteroid.GetComponent<Mover>().enabled = true;
                

                yield return asteroidWait;
            }
        }
        if (!GameOverFlag)
        {
            AsteroidPool();
        }

    }

    public void restart()
    {
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        if (PauseFlag == true)
        {
            PauseFlag = false;
        }

    }

    public void pauseGame()
    {
        if (!GameOverFlag)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseFlag = true;
            gameObject.GetComponent<AudioSource>().Pause();
            pauseButton.GetComponent<Image>().enabled = false;
        }

    }

    public void resumeGame()
    {
        pauseButton.GetComponent<Image>().enabled = true;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseFlag = false;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitTheGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SetHighScore();

        if (Score < 700)
            LoseText.text = "NOT BAD";
        else if (Score < 1000)
            LoseText.text = "NOICE!";
        else if (Score <= 3000)
            LoseText.text = "DAMN SON!";
        else if (Score > 3000 )
        {
            LoseText.text = "HOTDAMN! YOU WIN!";
            LoseText.color = new Color(250f, 0f, 70f);
        }

        pauseButton.GetComponent<Image>().enabled = false;

        GameOverText.enabled = true;
        LoseText.enabled = true;
        
        restartButtonImage.enabled = true;
        restartText.enabled = true;

        quitButtonImage.enabled = true;
        quitText.enabled = true;
    }

    private void SetHighScore()
    {
        if (PlayerPrefs.GetInt("High Score") < Score)
        {
            PlayerPrefs.SetInt("High Score", Score);
        }
    }

    public void scoreUpdate(bool AddToScore = true)
    {
        Score = AddToScore ? Score + asteroidShootScore : Score - asteroidShootScore;
    }

    public int GetScore()
    {
        return Score;
    }

    public void Mute()
    {
        AudioListener.volume = Muted ? 1.0f : 0.0f;
        Muted = !Muted;
    }
}