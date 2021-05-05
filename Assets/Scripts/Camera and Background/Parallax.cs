using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector2 length, startPos;
    public GameObject cam;
    public float effectIntensity;

    float screenHeight;
    float screenWidth;
    float backgroundWidth;
    float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer background = GetComponent<SpriteRenderer>();
        backgroundWidth = background.sprite.bounds.size.x;
        backgroundHeight = background.sprite.bounds.size.y;

        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = (screenHeight * Screen.width / Screen.height);

        transform.position = new Vector3(transform.position.x * screenWidth / backgroundWidth, transform.position.y * screenHeight / backgroundHeight, transform.position.z);
        startPos = transform.position;
        Vector2 bounds = GetComponent<SpriteRenderer>().bounds.size;
        length = new Vector2(bounds.x * screenWidth/backgroundWidth, bounds.y * screenHeight / backgroundHeight);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = new Vector2(cam.transform.position.x * (1 - effectIntensity), cam.transform.position.y * (1 - effectIntensity));
        Vector2 dist = new Vector2(cam.transform.position.x * effectIntensity, cam.transform.position.y * effectIntensity);

        transform.position = new Vector3(startPos.x + dist.x, startPos.y + dist.y, transform.position.z);

        if (temp.x > startPos.x + length.x)
        {
            startPos.x += length.x * 1.525f;
        }
        else if (temp.x < startPos.x - length.x)
        {
            startPos.x -= length.x * 1.525f;
        }

        if (temp.y > startPos.y + length.y)
        {
            startPos.y += length.y * 1.6f;
        }
        else if (temp.y < startPos.y - length.y)
        {
            startPos.y -= length.y * 1.6f;
        }
    }
}
