using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    UserInterface UI;

    int packagesCollected = 0;

    int packagesDelivered = 0;

    int enemiesKilled = 0;

    float playerHealth = 100;

    float timeLeft;

    [SerializeField]
    int collectPackagesScore = 10;

    [SerializeField]
    int deliveredPackagesScore = 10;

    [SerializeField]
    int enemiesKilledScore = 10;

    [SerializeField]
    int gameLength = 120;

    [SerializeField]
    Grapple grapple;

    [SerializeField]
    GameObject grid;

    [SerializeField]
    HealthBar healthBar;

    [HideInInspector]
    public bool isPlaying = false;

    [SerializeField]
    DeveloperTools devTools;

    bool invulnerable = false;

    bool timerActive = false;

    [SerializeField]
    IndicatorText indicatorText;

    [SerializeField]
    int[] indicatorTimes;

    Dictionary<int, bool> indicatorTimeMap;

    [SerializeField]
    GameObject music;

    Difficulty difficulty;

    Level level;

    [SerializeField]
    EnemySpawner spawner;

    [SerializeField]
    SpriteRenderer feature;

    [SerializeField]
    SpriteRenderer background;

    [SerializeField]
    GameObject backdrop; 

    void Awake()
    {
        Time.timeScale = 0;
        grapple.enabled = false;
        grid.SetActive(false);
        timeLeft = gameLength;
        indicatorTimeMap = new Dictionary<int, bool>();
        foreach (int time in indicatorTimes)
        {
            indicatorTimeMap.Add(time, true);
        }
    }

    void Update()
    {
        if (isPlaying && timerActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {               
                EndGame();
            }
            UI.UpdateTimer((int) timeLeft);
        }

        if (isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(false);       
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isPlaying)
            {
                Pause(true);
            }
            else
            {
                Resume();
            }
        }

        if (playerHealth <= 0)
        {
            EndGame();
        }

        int timeLeftWhole = (int)timeLeft;

        if (indicatorTimeMap.ContainsKey(timeLeftWhole) && indicatorTimeMap[timeLeftWhole])
        {
            indicatorTimeMap[timeLeftWhole] = false;
            indicatorText.ShowText(timeLeftWhole > 6 ? timeLeftWhole + " Seconds Left!" : timeLeftWhole.ToString(), (int)timeLeft < 6 ? 1 : 2);
        }
    }

    public void Pause(bool devTools)
    {
        isPlaying = false;
        Time.timeScale = 0;
        grapple.Ungrapple();
        grapple.enabled = false;
        UI.Pause(devTools);
    }

    public void Resume()
    {
        if (!devTools.easyMovementIsOn)
        {
            grapple.enabled = true;
        }

        UI.Resume();
        Time.timeScale = 1;
        isPlaying = true;
    }

    public void StartGame(float introLength)
    {
        // Use IntroLength
        grapple.enabled = true;
        grid.SetActive(true);
        Time.timeScale = 1;
        isPlaying = true;
        timerActive = true;
        UI.StartGame();
        music.GetComponents<AudioSource>()[0].Stop();
        music.GetComponents<AudioSource>()[1].Play();
    }

    public void PackageCollected()
    {
        packagesCollected++;
        UI.UpdateCollection(packagesCollected);
        indicatorText.ShowText("Package Collected!", 2);
    }

    public bool DeliverPackages()
    {
        if (packagesCollected > 0)
        {
            indicatorText.ShowText("Packages Delivered!", 2);
            packagesDelivered += packagesCollected;
            packagesCollected = 0;
            UI.UpdateCollection(packagesCollected);
            UI.UpdateDelivered(packagesDelivered);
            return true;
        }
        return false;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    void EndGame()
    {
        isPlaying = false;
        Time.timeScale = 0;
        int totalPackagesDelivered = packagesCollected + packagesDelivered;
        int score = (int) (((enemiesKilled * enemiesKilledScore) + (totalPackagesDelivered * collectPackagesScore) + (packagesDelivered * deliveredPackagesScore)) * difficulty.scoreMultiplier);
        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        music.SetActive(false);

        UI.EndGame(score, enemiesKilled, totalPackagesDelivered, packagesDelivered, difficulty.scoreMultiplier, DetermineAchievements(score, enemiesKilled));
        grapple.enabled = false;
        grid.SetActive(false);
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
        {
            cell.SetActive(false);
        }
        
        if (level.name == "Cloud")
        {
            PlayerPrefs.SetInt("cloudPlayed", 1);
        }

        if (level.name == "Space")
        {
            PlayerPrefs.SetInt("spacePlayed", 1);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleTimer(bool timerIsActive)
    {
        timerActive = timerIsActive;
    }

    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            playerHealth -= damage;
            healthBar.ChangeHealth(playerHealth);
            if (playerHealth < 10)
            {
                indicatorText.ShowText("Health Low!", 2);
            }
        }
    }

    public void ToggleInvulnerability(bool isInvulnerable)
    {
        invulnerable = isInvulnerable;
    }

    bool DetermineAchievements(float score, float enemiesKilled)
    {
        bool unlock = false;

        if (score >= 1000 && PlayerPrefs.GetInt("scoreAch3") != 1)
        {
            PlayerPrefs.SetInt("scoreAch3", 1);
            unlock = true;
        }

        if (score >= 5000 && PlayerPrefs.GetInt("scoreAch2") != 1)
        {
            PlayerPrefs.SetInt("scoreAch2", 1);
            unlock = true;
        }

        if (score >= 10000 && PlayerPrefs.GetInt("scoreAch1") != 1)
        {
            PlayerPrefs.SetInt("scoreAch1", 1);
            unlock = true;
        }

        if (enemiesKilled >= 15 && PlayerPrefs.GetInt("DDAch3") != 1)
        {
            PlayerPrefs.SetInt("DDAch3", 1);
            unlock = true;
        }

        if (enemiesKilled >= 20 && PlayerPrefs.GetInt("DDAch2") != 1)
        {
            PlayerPrefs.SetInt("DDAch2", 1);
            unlock = true;
        }

        if (enemiesKilled >= 25 && PlayerPrefs.GetInt("DDAch1") != 1)
        {
            PlayerPrefs.SetInt("DDAch1", 1);
            unlock = true;
        }

        return unlock;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        spawner.SetDifficulty(difficulty);
    }

    public void ChooseLevel(Level level)
    {
        this.level = level;
        feature.sprite = level.feature;
        background.sprite = level.background;
        foreach (SpriteRenderer renderer in backdrop.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.sprite = level.parallax;
        }
    }
}
