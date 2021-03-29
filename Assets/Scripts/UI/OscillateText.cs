using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OscillateText : MonoBehaviour
{
    [SerializeField]
    float minSize = 20;

    [SerializeField]
    float maxSize = 24;

    [SerializeField]
    float speed = 1;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float val = Mathf.Lerp(minSize, maxSize, (Mathf.Sin(Time.time * speed) + 1) / 2);
        text.transform.localScale = new Vector2(val, val);
    }
}
