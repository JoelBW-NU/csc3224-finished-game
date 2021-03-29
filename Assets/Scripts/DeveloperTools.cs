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

    public void TogglePlayerMovement(bool isOn)
    {
        playerMovement.enabled = isOn;
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
