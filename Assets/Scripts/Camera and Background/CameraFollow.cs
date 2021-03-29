using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float smoothSpeed = 0.125f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, -1);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.timeScale);
        transform.position = smoothPosition;
    }

    
}
