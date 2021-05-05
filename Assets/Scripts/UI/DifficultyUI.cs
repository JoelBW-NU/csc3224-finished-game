using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    [SerializeField]
    MainMenu mainMenu;

    [SerializeField]
    GameLogic game;

    [SerializeField]
    List<Button> difficultyButtons;

    [SerializeField]
    List<Difficulty> difficulties;

    // Start is called before the first frame update
    void OnEnable()
    {
        difficultyButtons[0].onClick.AddListener(delegate { SetDifficulty(difficulties[0]); });
        difficultyButtons[1].onClick.AddListener(delegate { SetDifficulty(difficulties[1]); });
        difficultyButtons[2].onClick.AddListener(delegate { SetDifficulty(difficulties[2]); });
        difficultyButtons[3].onClick.AddListener(delegate { SetDifficulty(difficulties[3]); });
    }

    void SetDifficulty(Difficulty diff)
    {
        game.SetDifficulty(diff);
        mainMenu.OpenLevel();
        gameObject.SetActive(false);
    }
}
