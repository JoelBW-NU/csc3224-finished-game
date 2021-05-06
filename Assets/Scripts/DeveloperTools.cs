using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperTools : MonoBehaviour
{
    [SerializeField]
    GameLogic game;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    Grapple grapple;

    [SerializeField]
    GameObject player;

    [SerializeField]
    EnemySpawner enemySpawner;

    public bool easyMovementIsOn = false;
    public bool timerIsPaused = false;
    public bool enemiesSpawn = true;
    public bool invulnerability = false;

    void Update()
    {
        if (game.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                TogglePlayerMovement(!easyMovementIsOn);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInvulnerability(!invulnerability);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                ToggleTimer(!timerIsPaused);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleEnemiesSpawn(!enemiesSpawn);
            }
        }    
    }

    public void TogglePlayerMovement(bool isOn)
    {
        playerMovement.enabled = isOn;
        grapple.Ungrapple();
        grapple.firstGrapple = false;
        grapple.enabled = !isOn;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().isKinematic = isOn;
        easyMovementIsOn = isOn;
    }

    public void ChangeMovementSpeed(float speed)
    {
        playerMovement.ChangeSpeed(speed);
    }

    public void ToggleInvulnerability(bool isOn)
    {
        invulnerability = isOn;
        game.ToggleInvulnerability(isOn);
    }

    public void ToggleTimer(bool isOn)
    {
        timerIsPaused = isOn;
        game.ToggleTimer(!isOn);
    }

    public void ToggleEnemiesSpawn(bool isOn)
    {
        enemiesSpawn = isOn;
        if (!isOn)
        {
            enemySpawner.GetComponent<EnemySpawner>().RemoveAllEnemies();
        }
        enemySpawner.enabled = isOn;
    }
}
