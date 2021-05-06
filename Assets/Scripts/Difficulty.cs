using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Difficulty : ScriptableObject
{
    public float scoreMultiplier;
    public int maxEnemies;
    public float speedMultiplier;
    public float enemyDamagePerHit;
    public float playerDamageMax;
}
