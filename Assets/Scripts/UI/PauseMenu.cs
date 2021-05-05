using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameLogic game;

    [SerializeField]
    Button resume;

    [SerializeField]
    Button devToolsButton;

    [SerializeField]
    GameObject devToolsPanel;

    [SerializeField]
    Button mainMenuButton;

    [SerializeField]
    Button optionsButton;

    [SerializeField]
    GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        resume.onClick.AddListener(Resume);
        devToolsButton.onClick.AddListener(OpenDeveloperTools);
        mainMenuButton.onClick.AddListener(OpenMainMenu);
        optionsButton.onClick.AddListener(OpenOptions);
    }

    void Resume()
    {
        game.Resume();
        gameObject.SetActive(false);
    }

    void OpenDeveloperTools()
    {
        devToolsPanel.SetActive(true);
    }

    void OpenMainMenu()
    {
        game.PlayAgain();
    }

    void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
}
