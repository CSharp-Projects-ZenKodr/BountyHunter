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
    public GameObject PauseMenuScreen, GameOverScreen, GameUIScreen;
    public GameObject Hazard;
    public Vector3 SpawnValues;
    public Vector3 AsteroidPoolPosition; /*new Vector3(60f, -2f, -1.5f);*/
    
    public static bool GameOverFlag, PauseFlag;
    public bool Muted;

    private AudioSource MusicSource;
    private int AsteroidShootScore, PlayerScore;
    private GameObject[] AsteroidsPool;


    //Class Methods
    private void Start()
    {
        Time.timeScale = 1f;

        PlayerScore = 0;
        AsteroidShootScore = 10;
        AsteroidsPool = new GameObject[20];
        MusicSource = GetComponent<AudioSource>();

        GameOverFlag = PauseFlag = Muted = false;

        MusicSource.Play();

        GameUIScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        PauseMenuScreen.SetActive(false);

        SetHighScore();

        AsteroidPool();
    }

    private void Update()
    {
        {
            Text temp = GameUIScreen.transform.GetChild(0).GetComponent<Text>();
            temp.text = "Score: " + PlayerScore;
        }

        if (GameOverFlag) { GameOver(); } 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseFlag)
                ResumeGame(); 
            else
                PauseGame(); 
        }
    }

    void AsteroidPool()
    {
        int AsteroidsCount = 20;

        for (int i = 0; i < AsteroidsCount; i++)
            AsteroidsPool[i] = Instantiate(Hazard, AsteroidPoolPosition, Quaternion.identity);

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        foreach (GameObject asteroid in AsteroidsPool)
        {
            if(!GameOverFlag)
            {
                WaitForSeconds AsteroidWait = new WaitForSeconds(Random.Range(0.3f, 1.75f));
                asteroid.transform.position = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                asteroid.transform.rotation = Quaternion.identity;
                asteroid.GetComponent<Mover>().enabled = true;
                yield return AsteroidWait;
            }
        }
        if (!GameOverFlag)
            AsteroidPool();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        if (!GameOverFlag)
        {
            PauseMenuScreen.SetActive(true);
            GameUIScreen.SetActive(false);
            Time.timeScale = 0f;
            PauseFlag = true;
            MusicSource.Pause();
        }

    }

    public void ResumeGame()
    {
        PauseMenuScreen.SetActive(false);
        GameUIScreen.SetActive(true);
        Time.timeScale = 1f;
        PauseFlag = false;
        MusicSource.Play();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SetHighScore();

        Text GameOverMessage = GameOverScreen.transform.GetChild(1).GetComponent<Text>();

        if (PlayerScore == PlayerPrefs.GetInt("High Score") && PlayerScore != 0)
        {
            GameOverMessage.text = "HIGH SCORE!";
        }
        else
        {
            if (PlayerScore >= 500 && PlayerScore < 1500)
                GameOverMessage.text = "Not bad";
            else if (PlayerScore >= 1500 && PlayerScore < 3000)
                GameOverMessage.text = "Pshttt you got lucky";
            else if (PlayerScore >= 3000)
                GameOverMessage.text = "DAMN SON!";
        }

        PauseMenuScreen.SetActive(false);
        GameUIScreen.SetActive(false);
        GameOverScreen.SetActive(true);
        GameOverFlag = true; //possibly useless. Check if removing break game.
    }

    private void SetHighScore()
    {
        if (PlayerPrefs.GetInt("High Score") < PlayerScore)
        { //Set High score. 
            PlayerPrefs.SetInt("High Score", PlayerScore);
        }

        //Update High Score on Game screen. 
        Text temp = GameUIScreen.transform.GetChild(1).GetComponent<Text>();
        temp.text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString();
    }

    public void ScoreUpdate(bool AddToScore = true)
    {
        PlayerScore = AddToScore ? PlayerScore + AsteroidShootScore : PlayerScore - AsteroidShootScore;
    }

    public int GetScore()
    {
        return PlayerScore;
    }

    public void Mute()
    {
        AudioListener.volume = Muted ? 1.0f : 0.0f;
        Muted = !Muted;
    }
}