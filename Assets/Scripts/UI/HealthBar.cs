using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Text healthAmount;

    [SerializeField]
    RectTransform indicatorBar;

    float initialWidth;

    void Start()
    {
        initialWidth = indicatorBar.sizeDelta.x;
    }

    public void ChangeHealth(float newHealth)
    {
        int healthInt = (int) newHealth;
        if (healthInt < 0) healthInt = 0;
        healthAmount.text = healthInt.ToString() + "/100";
        indicatorBar.sizeDelta = new Vector2(newHealth/100 * initialWidth, indicatorBar.sizeDelta.y);
    }
}
