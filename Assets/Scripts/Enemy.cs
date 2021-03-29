using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 100;

    [SerializeField]
    float enemyDamagePerHit = 35;

    [SerializeField]
    Rigidbody2D player;

    [SerializeField]
    float moveSpeedMultiplier = 350f;

    [SerializeField]
    float minSpeed = 5;

    [SerializeField]
    float rotateSpeed = 2000f;

    Rigidbody2D rb;

    [SerializeField]
    float slowMoMultiplier = 5;

    float slowMoFactor = 1;

    bool dead = false;

    [HideInInspector]
    public EnemySpawner spawner;

    [HideInInspector]
    public GameLogic game;

    [SerializeField]
    float smoothSpeed = 0.125f;

    [SerializeField]
    float playerDamageOverTime = 0.01f;

    [SerializeField]
    Transform healthIndicator;

    float initialIndicatorSize;

    bool onScreen = false;

    AudioSource enemyDamageSoundEffect;
    AudioSource playerDamageSoundEffect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponentsInChildren<ParticleSystem>()[1].Stop();
        rotateSpeed *= Random.Range(0.6f, 2f);
        initialIndicatorSize = healthIndicator.transform.localScale.x;
        enemyDamageSoundEffect = GetComponent<AudioSource>();
        playerDamageSoundEffect = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            if (Time.timeScale != 1)
            {
                slowMoFactor = slowMoMultiplier;
            }
            else
            {
                slowMoFactor = 1;
            }

            float speed;
            float dist = Mathf.Clamp(Vector2.Distance(transform.position, player.position), 1, 1.2f);

            if (onScreen)
            {
                speed = player.velocity.magnitude * moveSpeedMultiplier * dist * Time.deltaTime * slowMoFactor;
            } 
            else
            {
                speed = moveSpeedMultiplier * Time.deltaTime * slowMoFactor * dist;
            }
         
            rb.velocity = Vector2.Lerp(rb.velocity, transform.up * (speed > minSpeed ? speed : minSpeed), Time.deltaTime * smoothSpeed);
            Vector3 targetVector = player.transform.position - transform.position;
            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
            rb.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.deltaTime * slowMoFactor;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Projectile") && !dead)
        {
            health -= enemyDamagePerHit;
            if (health < 0) health = 0;
            healthIndicator.localScale = new Vector3(health / 100 * initialIndicatorSize, healthIndicator.localScale.y, healthIndicator.localScale.z);           
            if (health <= 0)
            {
                spawner.RemoveEnemy(gameObject);
                dead = true;
                rb.velocity = new Vector2(0, 0);
                GetComponentInChildren<ParticleSystem>().Play();
                GetComponent<SpriteRenderer>().enabled = false;
                game.EnemyKilled();
                Destroy(gameObject, 4);
            } 
            else
            {
                GetComponentsInChildren<ParticleSystem>()[1].Play();
            }
            enemyDamageSoundEffect.Play();
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Screen Bounds"))
        {
            onScreen = true;
            rb.velocity = Vector2.zero;
        }

        if (collision.CompareTag("Player") && !dead)
        {
            playerDamageSoundEffect.Play();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !dead)
        {
            game.TakeDamage(playerDamageOverTime);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Screen Bounds"))
        {
            onScreen = false;
        }
    }
}
