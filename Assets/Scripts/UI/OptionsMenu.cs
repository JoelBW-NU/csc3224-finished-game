using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    Options options;

    [SerializeField]
    Toggle soundToggle;

    [SerializeField]
    Toggle musicToggle;

    [SerializeField]
    Toggle aimLineToggle;

    [SerializeField]
    Button backButton;

    void Start()
    {
        soundToggle.onValueChanged.AddListener(delegate
        {
            options.UpdateSound(soundToggle.isOn);
        });

        musicToggle.onValueChanged.AddListener(delegate
        {
            options.UpdateMusic(musicToggle.isOn);
        });

        aimLineToggle.onValueChanged.AddListener(delegate
        {
            options.UpdateAimLine(aimLineToggle.isOn);
        });
    }

    void OnEnable()
    {
        soundToggle.isOn = options.GetSoundOn();
        musicToggle.isOn = options.GetMusicOn();
        aimLineToggle.isOn = options.GetAimLineVisible();
        backButton.interactable = true;
    }

    void OnDisable()
    {
        backButton.interactable = false;
    }
}
