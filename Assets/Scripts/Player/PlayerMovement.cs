using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 tempVect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += tempVect * speed * Time.deltaTime;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
