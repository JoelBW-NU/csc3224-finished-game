using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField]
    GameObject music;

    [SerializeField]
    FireProjectile aimLine;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("soundOn"))
        {
            PlayerPrefs.SetInt("soundOn", 1);
        }

        if (!PlayerPrefs.HasKey("musicOn"))
        {
            PlayerPrefs.SetInt("musicOn", 1);
        }

        if (!PlayerPrefs.HasKey("aimLine"))
        {
            PlayerPrefs.SetInt("aimLine", 0);
        }

        UpdateSound(PlayerPrefs.GetInt("soundOn") == 1);
        UpdateMusic(PlayerPrefs.GetInt("musicOn") == 1);
        UpdateAimLine(PlayerPrefs.GetInt("aimLine") == 1);
    }

    public bool GetSoundOn()
    {
        return PlayerPrefs.GetInt("soundOn") == 1;
    }

    public bool GetMusicOn()
    {
        return PlayerPrefs.GetInt("musicOn") == 1;
    }

    public bool GetAimLineVisible()
    {
        return PlayerPrefs.GetInt("aimLine") == 1;
    }

    public void UpdateSound(bool soundOn)
    {
        PlayerPrefs.SetInt("soundOn", soundOn == true ? 1 : 0);

        if (soundOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void UpdateMusic(bool musicOn)
    {
        PlayerPrefs.SetInt("musicOn", musicOn == true ? 1 : 0);

        if (musicOn)
        {
            foreach (AudioSource music in music.GetComponents<AudioSource>()) {
                music.volume = 0.2f;
            }
        }
        else
        {
            foreach (AudioSource music in music.GetComponents<AudioSource>())
            {
                music.volume = 0;
            }
        }
    }

    public void UpdateAimLine(bool aimLineVisible)
    {
        PlayerPrefs.SetInt("aimLine", aimLineVisible == true ? 1 : 0);
        aimLine.showAimLine = aimLineVisible;
    }
}
