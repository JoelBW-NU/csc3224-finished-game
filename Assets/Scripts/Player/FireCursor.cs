using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCursor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
