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

    LineRenderer aimLine;

    [HideInInspector]
    public bool showAimLine;

    void Start()
    {
        nextFire = Time.time;
        soundEffect = GetComponent<AudioSource>();
        aimLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        if (showAimLine)
        {
            aimLine.SetPositions(new Vector3[] { transform.position, Camera.main.ScreenToWorldPoint(mousePos) });
        }
        else
        {
            aimLine.SetPositions(new Vector3[] { transform.position, transform.position });
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            Vector2 aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(aimDirection.normalized * force, ForceMode2D.Impulse);
            nextFire = Time.time + fireRate;
            soundEffect.Play();
        }
    }
}
