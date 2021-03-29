using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    Text collectionText;

    [SerializeField]
    Text deliveredText;

    [SerializeField]
    Text timer;

    [SerializeField]
    Text enemiesKilledText;

    [SerializeField]
    Text packagesCollectedText;

    [SerializeField]
    Text packagesDeliveredText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    GameObject gameInfo;

    [SerializeField]
    Button playAgain;

    [SerializeField]
    GameLogic game;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    Texture2D cursorTexture;

    [SerializeField]
    GameObject indicatorText;

    void Start()
    {
        playAgain.onClick.AddListener(PlayAgain);       
        indicatorText.SetActive(false);
    }

    public void StartGame()
    {
        indicatorText.SetActive(true);
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.height / 2), CursorMode.Auto);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Resume()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.height / 2), CursorMode.Auto);
    }

    public void UpdateCollection(int packagesCollected)
    {
        collectionText.text = "Packages Collected: " + packagesCollected;
    }

    public void UpdateDelivered(int packagesDelivered)
    {
        deliveredText.text = "Packages Delivered: " + packagesDelivered;
    }

    public void UpdateTimer(int timeToDisplay)
    {
        float mins = Mathf.Floor(timeToDisplay / 60);
        float secs = Mathf.RoundToInt(timeToDisplay % 60);

        timer.text = "Time Left: " + (mins < 10 ? "0" + mins.ToString() : mins.ToString()) + ":" + (secs < 10 ? "0" + Mathf.RoundToInt(secs).ToString() : secs.ToString());
    }

    public void EndGame(int score, int enemiesKilled, int packagesCollected, int packagesDelivered)
    {
        gameOver.SetActive(true);
        gameInfo.SetActive(false);
        enemiesKilledText.text= enemiesKilled.ToString();
        packagesCollectedText.text = packagesCollected.ToString();
        packagesDeliveredText.text = packagesDelivered.ToString();
        scoreText.text = "Overall Score: " + score.ToString();
    }

    void PlayAgain()
    {
        game.PlayAgain();
    }
}
