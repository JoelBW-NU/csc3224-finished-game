using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorText : MonoBehaviour
{
    float fadeTime = 2;
    float fadeValue = 0;

    [SerializeField]
    float textIncreaseAmount = 1.2f;

    Vector2 originalTextScale;

    [SerializeField]
    Text textItem;

    bool fade = false;

    // Start is called before the first frame update
    void Start()
    {
        originalTextScale = new Vector2(textItem.transform.localScale.x, textItem.transform.localScale.y);
        textItem.color = new Color(textItem.color.r, textItem.color.g, textItem.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            textItem.color = new Color(textItem.color.r, textItem.color.g, textItem.color.b, fadeValue / fadeTime);
            fadeValue -= Time.deltaTime;
            textItem.transform.localScale = originalTextScale * Mathf.Lerp(1, textIncreaseAmount, (fadeTime - fadeValue) / fadeTime);

            if (fadeValue <= 0)
            {
                fade = false;
            }
        }
    }

    public void ShowText(string text, float timeToFade)
    {
        textItem.text = text;
        textItem.color = new Color(textItem.color.r, textItem.color.g, textItem.color.b, 1);
        fadeTime = timeToFade;
        fadeValue = timeToFade;
        fade = true;
    }
}
