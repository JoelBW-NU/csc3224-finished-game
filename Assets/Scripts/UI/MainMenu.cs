using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Image title;
    
    [SerializeField]
    Button play;

    [SerializeField]
    Image menu;

    [SerializeField]
    Button controls;

    [SerializeField]
    float timeToFade = 3;

    float fadeCounter = 0;

    bool fade = false;

    [SerializeField]
    GameLogic game;

    [SerializeField]
    public Texture2D cursor;

    [SerializeField]
    Text highScore;

    [SerializeField]
    Button quit;

    [SerializeField]
    Button howToPlay;

    [SerializeField]
    GameObject howToPlayPanel;

    [SerializeField]
    Button howToPlayBack;

    [SerializeField]
    GameObject controlsPanel;

    [SerializeField]
    Button controlsBack;

    [SerializeField]
    Image devToolsPanel;

    [SerializeField]
    Text devToolsText;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(Play);
        howToPlay.onClick.AddListener(OpenHowToPlay);
        controls.onClick.AddListener(OpenControls);
        howToPlayBack.onClick.AddListener(CloseHowToPlay);
        controlsBack.onClick.AddListener(CloseControls);
        quit.onClick.AddListener(Quit);
        highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0);
    }

    void Play()
    {
        fade = true;
        play.interactable = false;
        howToPlay.interactable = false;
        controls.interactable = false;
        howToPlayBack.interactable = false;
        controls.interactable = false;
        quit.interactable = false;
        fadeCounter = timeToFade;
        game.StartGame(timeToFade);    
    }

    void OpenHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    void CloseControls()
    {
        controlsPanel.SetActive(false);
    }

    void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (fade)
        {
            fadeCounter -= Time.deltaTime;
            Color fadeCol = new Color(menu.color.r, menu.color.g, menu.color.b, fadeCounter / timeToFade);
            menu.color = fadeCol;
            title.color = fadeCol;
            play.GetComponent<Image>().color = fadeCol;
            play.GetComponentInChildren<Text>().color = fadeCol;
            howToPlay.GetComponent<Image>().color = fadeCol;
            howToPlay.GetComponentInChildren<Text>().color = fadeCol;
            controls.GetComponent<Image>().color = fadeCol;
            controls.GetComponentInChildren<Text>().color = fadeCol;
            quit.GetComponent<Image>().color = fadeCol;
            quit.GetComponentInChildren<Text>().color = fadeCol;
            devToolsPanel.color = fadeCol;
            devToolsText.color = fadeCol;
            highScore.color = fadeCol;
            if (fadeCounter <= 0)
            {
                menu.enabled = false;
                play.enabled = false;
            }                          
        }
    }
}
