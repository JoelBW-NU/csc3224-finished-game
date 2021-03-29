using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundHandler : MonoBehaviour
{
    float screenHeight;
    float backgroundWidth;
    float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer background = GetComponent<SpriteRenderer>();

        backgroundWidth = background.sprite.bounds.size.x;
        backgroundHeight = background.sprite.bounds.size.y;

        screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = (screenHeight * Screen.width / Screen.height);

        transform.localScale = new Vector3(screenWidth / backgroundWidth, screenHeight / backgroundHeight, 0);
    }

    void Update()
    {
        if (screenHeight != Camera.main.orthographicSize * 2)
        {
            screenHeight = Camera.main.orthographicSize * 2;
            float screenWidth = (screenHeight * Screen.width / Screen.height);

            transform.localScale = new Vector3(screenWidth / backgroundWidth, screenHeight / backgroundHeight, 0);
        }
    }
}
