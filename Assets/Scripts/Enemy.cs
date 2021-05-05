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
    float playerDamageMax = 8;

    [SerializeField]
    float playerDamageMin = 3;

    [SerializeField]
    Transform healthIndicator;

    float initialIndicatorSize;

    AudioSource enemyDamageSoundEffect;
    AudioSource playerDamageSoundEffect;

    [SerializeField]
    float damageTime = 0.5f;

    float damageCounter = 0;

    bool playerContact = false;

    [SerializeField]
    float slowDownDist;

    bool nearPlayer;

    [SerializeField]
    float maxDistFromPlayer = 100;

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

    void Update()
    {
        if (playerContact && damageCounter < damageTime)
        {
            damageCounter += Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distFromPlayer = Vector2.Distance(transform.position, player.position);

        if (distFromPlayer > maxDistFromPlayer)
        {
            spawner.RemoveEnemy(gameObject);
            GetComponentsInChildren<ParticleSystem>()[2].Stop();
            dead = true;
            rb.velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }

        if (!nearPlayer && distFromPlayer <= slowDownDist)
        {
            nearPlayer = true;
            rb.velocity = Vector2.zero;
        } 
        else if (nearPlayer && distFromPlayer > slowDownDist)
        {
            nearPlayer = false;
        }

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

            float dist = Mathf.Clamp(distFromPlayer, 1, 1.25f);
            float speed;

            if (nearPlayer)
            {
                speed = player.velocity.magnitude * moveSpeedMultiplier * dist * Time.deltaTime * slowMoFactor;
            }
            else
            {
                speed = moveSpeedMultiplier * Time.deltaTime * slowMoFactor * dist * 20;
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
                GetComponentsInChildren<ParticleSystem>()[2].Stop();
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

        if (collision.CompareTag("Player") && !dead)
        {
            game.TakeDamage(Random.Range(playerDamageMin, playerDamageMax));
            playerContact = true;
            playerDamageSoundEffect.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerContact = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !dead && damageCounter >= damageTime)
        {
            game.TakeDamage(Random.Range(playerDamageMin, playerDamageMax));
            damageCounter = 0;
        }
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        moveSpeedMultiplier = difficulty.speedMultiplier;
        enemyDamagePerHit = difficulty.enemyDamagePerHit;
        playerDamageMax = difficulty.playerDamageMax;
    }
}
