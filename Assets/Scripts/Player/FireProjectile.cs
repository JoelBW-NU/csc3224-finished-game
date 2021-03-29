using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float force = 5;

    [SerializeField]
    float fireRate = 0.5f;

    float nextFire;

    AudioSource soundEffect;

    void Start()
    {
        nextFire = Time.time;
        soundEffect = GetComponents<AudioSource>()[1];
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            Vector2 aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(aimDirection.normalized * force, ForceMode2D.Impulse);
            nextFire = Time.time + fireRate;
            soundEffect.Play();
        }
    }
}
