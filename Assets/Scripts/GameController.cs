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
    public Text gameOver;
    public Text restartText;
    public Text quitText;
    public GameObject backgroundCapsule;
    public Text LoseText;
    public GameObject hazard;
    public Vector3 SpawnValues;
    public Text scoreDisplay;
    public Button quitButton;
    public Button restartButton;
    public Image restartButtonImage;
    public Image quitButtonImage;
    public GameObject PauseMenu;
    public GameObject pauseButton;
    public GameObject HighScore;

   
    [HideInInspector] public bool gameOverFlag = false;
    [HideInInspector] public static bool pauseFlag = false;
    [HideInInspector] public static bool raiseSpeedFlag = true;

    private int asteroidShootScore = 10;
    private int SCORE = 0;
    private float winningScore = Mathf.Infinity;
    private Quaternion spawnRotation;
    private Vector3 spawnPositon;
    private WaitForSeconds asteroidWait;
    private WaitForSeconds backgroundWait;
    private GameObject[] asteroids = new GameObject[20];
    private bool muteFlag = false;
    


    private void Awake()
    {
        Time.timeScale = 1f;
        //PlayerPrefs.SetInt("High Score", SCORE);
              
    }

    private void Start()
    {
        pauseFlag = false;
        gameObject.GetComponent<AudioSource>().Play();
        AsteroidPool();
    }

    private void Update()
    {
        HighScore.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString();

        scoreDisplay.text = "Score: " + System.Convert.ToString(SCORE);
        if (SCORE >= winningScore)
        {
            gameOverFlag = true;
            Mover.flag = true; //static GameOver Flag in the Mover script. 
            
        }

        if (gameOverFlag == true)
        {
            Invoke("GAMEOVER", 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (pauseFlag)
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
            if(!gameOverFlag)
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
        if (!gameOverFlag)
        {
            AsteroidPool();
        }

    }

    public void restart()
        {
            SceneManager.LoadScene("Main");
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        if (pauseFlag == true)
        {
            pauseFlag = false;
        }

        }

    public void pauseGame()
    {
        if (!gameOverFlag)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pauseFlag = true;
            gameObject.GetComponent<AudioSource>().Pause();
            pauseButton.GetComponent<Image>().enabled = false;
        }
        


    }

    public void resumeGame()
    {
        pauseButton.GetComponent<Image>().enabled = true;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseFlag = false;
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

    public void GAMEOVER()
    {
        if (PlayerPrefs.GetInt("High Score") < SCORE )
        {
            PlayerPrefs.SetInt("High Score", SCORE);
        }
        
        gameOver.enabled = true;
        pauseButton.GetComponent<Image>().enabled = false;

        if (SCORE < 200)
        {
            LoseText.enabled = true;
        }
        else if (200 <= SCORE && SCORE < 700)
        {
            LoseText.text = "NOT BAD";
            LoseText.enabled = true;
        }
        else if (700 <= SCORE && SCORE < 1000)
        {
            LoseText.text = "NOICE!";
            LoseText.enabled = true;
        }
        else if (1000 <= SCORE && SCORE < 3000)
        {
            LoseText.text = "DAMN SON!";
            LoseText.enabled = true;
        }
        else if (SCORE > 3000 )
        {
            LoseText.text = "HOTDAMN! YOU WIN!";
            LoseText.color = new Color(255f, 0f, 0f);
            LoseText.enabled = true;
        }
        restartButtonImage.enabled = true;
        restartText.enabled = true;
        quitButtonImage.enabled = true;
        quitText.enabled = true;


    }

    public void scoreUpdate()
    {
        SCORE += asteroidShootScore;
    }

    public void scoreUpdate(bool dropScore)
    {
        SCORE -= asteroidShootScore;
    }

    public int GetScore()
    {
        return SCORE;
    }

    public void turnOffMusic()
    {
        if (muteFlag)
        {
            AudioListener.volume = 1.0f;
        }
        else if (!muteFlag)
        {
            AudioListener.volume = 0.0f;
        }
        muteFlag = !muteFlag;
    }
}