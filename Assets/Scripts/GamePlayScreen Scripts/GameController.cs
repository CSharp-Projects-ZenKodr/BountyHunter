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
    private void Awake()
    {
        Time.timeScale = 1f;
        GameOverFlag = PauseFlag = Muted = false;
        PlayerScore = 0;
        AsteroidShootScore = 10;
        AsteroidsPool = new GameObject[20];
        PlayerPrefs.SetInt("High Score", PlayerScore);
        MusicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        MusicSource.Play();

        GameUIScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        PauseMenuScreen.SetActive(false);

        AsteroidPool();
    }

    private void Update()
    {
        {
            Text temp = GameUIScreen.transform.GetChild(0).GetComponent<Text>();
            temp.text = "Score: " + System.Convert.ToString(PlayerScore);
        }

        if (GameOverFlag)
            GameOver();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseFlag) { ResumeGame(); }
            else { PauseGame(); } 
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

        if (PlayerScore == PlayerPrefs.GetInt("High Score"))
        {
            GameOverMessage.text = "HIGH SCORE!";
        }
        else
        {
            if (PlayerScore < 700)
                GameOverMessage.text = "Not bad";
            else if (PlayerScore < 1500)
                GameOverMessage.text = "Pshttt you got lucky";
            else if (PlayerScore <= 3000)
                GameOverMessage.text = "DAMN SON!";
            else if (PlayerScore > 3000)
            {
                GameOverMessage.text = "HOTDAMN! YOU WIN!";
                GameOverMessage.color = new Color(250f, 0f, 70f);
            }
        }

        PauseMenuScreen.SetActive(false);
        GameUIScreen.SetActive(false);
        GameOverScreen.SetActive(true);
        GameOverFlag = true;
    }

    private void SetHighScore()
    {
        if (PlayerPrefs.GetInt("High Score") < PlayerScore)
        { //Set High score. Update High Score on Game screen. 
            PlayerPrefs.SetInt("High Score", PlayerScore);
            Text temp = GameUIScreen.transform.GetChild(1).GetComponent<Text>();
            temp.text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString();
        }
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
        MusicSource.volume = Muted ? 1.0f : 0.0f;
        //AudioListener.volume = Muted ? 1.0f : 0.0f;
        Muted = !Muted;
    }
}