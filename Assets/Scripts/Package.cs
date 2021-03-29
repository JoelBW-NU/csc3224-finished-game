using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    [HideInInspector]
    public GameLogic game;

    [HideInInspector]
    public PackageSpawner packageSpawner;

    bool canCollectPackage = false;

    [HideInInspector]
    public GameObject collectPackageText;

    AudioSource soundEffect;

    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canCollectPackage && Input.GetKey(KeyCode.E))
        {
            canCollectPackage = false;
            GetComponent<CircleCollider2D>().enabled = false;
            collectPackageText.SetActive(false);
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            game.PackageCollected();
            packageSpawner.SpawnPackage();
            Destroy(gameObject, 4);
            soundEffect.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            canCollectPackage = true;
            collectPackageText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            canCollectPackage = false;
            collectPackageText.SetActive(false);
        }
    }
}
