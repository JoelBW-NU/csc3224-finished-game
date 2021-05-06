using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackgroundScroll : MonoBehaviour
{
    [SerializeField]
    RectTransform backOne;

    [SerializeField]
    RectTransform backTwo;

    [SerializeField]
    float rate;

    [SerializeField]
    bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVec = new Vector3((movingRight ? 1 : -1) * Time.unscaledDeltaTime * rate, 0, 0);
        backOne.localPosition += movementVec;
        backTwo.localPosition += movementVec;

        if (movingRight && backOne.localPosition.x >= Screen.width)
        {
            backOne.localPosition = new Vector3(-Screen.width, 0, 0);
        }

        if (movingRight && backTwo.localPosition.x >= Screen.width)
        {
            backTwo.localPosition = new Vector3(-Screen.width, 0, 0);
        }

        if (!movingRight && backOne.localPosition.x <= -Screen.width)
        {
            backOne.localPosition = new Vector3(Screen.width, 0, 0);
        }

        if (!movingRight && backTwo.localPosition.x <= -Screen.width)
        {
            backTwo.localPosition = new Vector3(Screen.width, 0, 0);
        }
    }
}
