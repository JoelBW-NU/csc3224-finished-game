using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerPosition : MonoBehaviour
{
    [SerializeField]
    float rightPos;

    [SerializeField]
    float leftPos;

    [SerializeField]
    RectTransform packageText;

    [SerializeField]
    RectTransform baseText;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < Screen.width/2)
        {
            packageText.localPosition = new Vector3(rightPos, packageText.localPosition.y, packageText.localPosition.z);
            packageText.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            baseText.localPosition = new Vector3(rightPos, baseText.localPosition.y, baseText.localPosition.z);
            baseText.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        }
        else
        {
            packageText.localPosition = new Vector3(leftPos, packageText.transform.localPosition.y, packageText.localPosition.z);
            packageText.GetComponent<Text>().alignment = TextAnchor.UpperRight;
            baseText.localPosition = new Vector3(leftPos, baseText.transform.localPosition.y, baseText.transform.localPosition.z);
            baseText.GetComponent<Text>().alignment = TextAnchor.UpperRight;
        }
    }
}
