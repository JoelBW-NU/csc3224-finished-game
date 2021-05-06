using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [HideInInspector]
    public Grapple grapple;

    [HideInInspector]
    public bool grappled = false;

    [HideInInspector]
    public bool countdownBegun = false;

    [SerializeField]
    float grappleLifetime = 3;

    float elapsedTime = 0;

    [SerializeField]
    Text countdownText;

    SpriteRenderer glow;

    void Start()
    {
        countdownText.text = "";
        glow = GetComponentsInChildren<SpriteRenderer>()[1];
    }

    void OnMouseOver()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (grapple.Pull(gameObject))
                {
                    grappled = true;
                    countdownBegun = true;
                    transform.parent = null;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (grapple.Swing(gameObject))
                {
                    grappled = true;
                    countdownBegun = true;
                    transform.parent = null;
                }
            }

            glow.enabled = true;
        }       
    }

    void OnMouseExit()
    {
        if (Time.timeScale != 0)
        {
            glow.enabled = false;
        }
    }

    void Update()
    {
        if (countdownBegun)
        {
            elapsedTime += Time.deltaTime;
            countdownText.text = ((int) (grappleLifetime - elapsedTime) + 1).ToString();
        }
        
        if (elapsedTime >= grappleLifetime)
        {          
            DestroyPoint();
        }
    }

    public void DestroyPoint()
    {
        if (grappled)
        {
            grapple.Ungrapple();
        }
        Destroy(gameObject);
    }
}
