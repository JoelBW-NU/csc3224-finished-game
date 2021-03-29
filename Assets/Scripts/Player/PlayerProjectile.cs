using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 3);
        }
    }
}
