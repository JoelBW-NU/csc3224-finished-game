using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPanel : MonoBehaviour
{
    [SerializeField]
    GameObject[] scoreAch;

    [SerializeField]
    GameObject[] ddAch;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("scoreAch3") == 1)
        {
            Unlock(scoreAch[2]);
        }

        if (PlayerPrefs.GetInt("scoreAch2") == 1)
        {
            Unlock(scoreAch[1]);
        }

        if (PlayerPrefs.GetInt("scoreAch1") == 1)
        {
            Unlock(scoreAch[0]);
        }

        if (PlayerPrefs.GetInt("DDAch3") == 1)
        {
            Unlock(ddAch[2]);
        }

        if (PlayerPrefs.GetInt("DDAch2") == 1)
        {
            Unlock(ddAch[1]);
        }

        if (PlayerPrefs.GetInt("DDAch1") == 1)
        {
            Unlock(ddAch[0]);
        }
    }

    void Unlock(GameObject ach)
    {
        ach.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        ach.GetComponentsInChildren<Image>()[1].enabled = false;
    }
}
