using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    float screenHalfHeight;
    BoxCollider2D screenDetector;

    // Start is called before the first frame update
    void Start()
    {
        screenDetector = GetComponent<BoxCollider2D>();

        screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = (screenHalfHeight * Screen.width / Screen.height);  

        screenDetector.size = new Vector2(screenHalfWidth * 2, screenHalfHeight * 2);
    }

    void Update()
    {
        if (screenHalfHeight != Camera.main.orthographicSize)
        {
            screenHalfHeight = Camera.main.orthographicSize;
            float screenHalfWidth = (screenHalfHeight * Screen.width / Screen.height);

            screenDetector.size = new Vector2(screenHalfWidth * 2, screenHalfHeight * 2);
        }
    }
}
