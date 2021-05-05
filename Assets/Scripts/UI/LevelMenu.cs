using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    MainMenu mainMenu;

    [SerializeField]
    GameLogic game;

    [SerializeField]
    List<Button> levelButtons;

    [SerializeField]
    List<Level> levels;

    // Start is called before the first frame update
    void OnEnable()
    {
        levelButtons[0].onClick.AddListener(delegate { ChooseLevel(levels[0]); });

        levelButtons[1].interactable = false;
        levelButtons[2].interactable = false;

        if (PlayerPrefs.GetInt("cloudPlayed") == 1)
        {
            levelButtons[1].onClick.AddListener(delegate { ChooseLevel(levels[1]); });
            levelButtons[1].interactable = true;
            levelButtons[1].GetComponentsInChildren<Image>()[1].enabled = false;
        }

        if (PlayerPrefs.GetInt("spacePlayed") == 1)
        {
            levelButtons[2].onClick.AddListener(delegate { ChooseLevel(levels[2]); });
            levelButtons[2].interactable = true;
            levelButtons[2].GetComponentsInChildren<Image>()[1].enabled = false;
        }    
    }

    void ChooseLevel(Level level)
    {
        game.ChooseLevel(level);
        gameObject.SetActive(false);
        mainMenu.Play();
    }
}
