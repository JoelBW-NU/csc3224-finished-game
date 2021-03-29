using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    GameLogic game;

    AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (game.DeliverPackages())
            {
                soundEffect.Play();
            }
        }
    }
}
