using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperToolsUI : MonoBehaviour
{
    [SerializeField]
    DeveloperTools tools;

    [SerializeField]
    Button returnButton;

    [SerializeField]
    Toggle easyMoveToggle;

    [SerializeField]
    Slider speedSlider;

    [SerializeField]
    Toggle invulnerabilityToggle;

    [SerializeField]
    Toggle pauseTimerToggle;

    [SerializeField]
    Toggle spawnEnemiesToggle;

    [SerializeField]
    Button easyMoveExplainButton;

    [SerializeField]
    GameObject easyMoveExplainPanel;

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.interactable = easyMoveToggle.isOn;
        returnButton.onClick.AddListener(Return);

        easyMoveExplainButton.onClick.AddListener(ToggleEasyMoveExplain);

        easyMoveToggle.onValueChanged.AddListener(delegate
        {
            ToggleEasyMove();
        });

        speedSlider.onValueChanged.AddListener(delegate
        {
            ChangeMovementSpeed();
        });

        invulnerabilityToggle.onValueChanged.AddListener(delegate
        {
            ToggleInvulnerability();
        });

        pauseTimerToggle.onValueChanged.AddListener(delegate
        {
            ToggleTimer();
        });

        spawnEnemiesToggle.onValueChanged.AddListener(delegate
        {
            ToggleEnemies();
        });

    }

    void Return()
    {
        gameObject.SetActive(false);
    }

    void ToggleEasyMove()
    {
        tools.TogglePlayerMovement(easyMoveToggle.isOn);
        speedSlider.interactable = easyMoveToggle.isOn;
        tools.ChangeMovementSpeed(speedSlider.value);
    }

    void ChangeMovementSpeed()
    {
        tools.ChangeMovementSpeed(speedSlider.value);
    }

    void ToggleInvulnerability()
    {
        tools.ToggleInvulnerability(invulnerabilityToggle.isOn);
    }

    void ToggleTimer()
    {
        tools.ToggleTimer(pauseTimerToggle.isOn);
    }

    void ToggleEnemies()
    {
        tools.ToggleEnemiesSpawn(spawnEnemiesToggle.isOn);
    }

    void ToggleEasyMoveExplain()
    {
        easyMoveExplainPanel.SetActive(!easyMoveExplainPanel.activeSelf);
        if (easyMoveExplainPanel.activeSelf)
        {
            easyMoveExplainButton.GetComponentInChildren<Text>().text = "X";
        }
        else
        {
            easyMoveExplainButton.GetComponentInChildren<Text>().text = "?";
        }
    }
}
