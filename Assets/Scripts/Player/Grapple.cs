using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    Rigidbody2D player;

    LineRenderer grappleLine;

    SpringJoint2D joint;

    GameObject activePoint;

    bool grappled = false;
    bool swing = false;
    bool pull = false;
    bool grappleFrame = false;

    float pullTimer = 0;
    float pullTime = 1;

    [SerializeField]
    float pullForce = 5;

    [SerializeField]
    float force = 5;

    [SerializeField]
    float maxVelocity = 8;

    Vector2 grapplePos;

    Vector2 pullDirection;

    [SerializeField]
    float ropeChangeRate = 0.01f;

    bool firstGrapple = true;

    AudioSource soundEffect;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        grappleLine = GetComponent<LineRenderer>();
        soundEffect = GetComponent<AudioSource>();
        grappleLine.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (grappled)
        {
            grappleLine.SetPositions(new Vector3[] { transform.position, grapplePos });          
        }

        if (swing && Input.GetMouseButtonUp(0) && !grappleFrame)
        {
            Ungrapple();
            swing = false;
        }

        if (pull && Input.GetMouseButtonUp(1) && !grappleFrame)
        {
            Ungrapple();
            pull = false;
            pullTimer = 0;
        }

        if (pull)
        {
            pullTimer += Time.deltaTime;
            if (pullTimer >= pullTime)
            {                
                Ungrapple();
                pull = false;
                pullTimer = 0;
            }           
        }     

       if (grappleFrame)
        {
            grappled = true;
            grappleFrame = false;
        }
    }

    void FixedUpdate()
    {
        if (grappled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                joint.distance -= ropeChangeRate;
            }

            if (Input.GetKey(KeyCode.S))
            {
                joint.distance += ropeChangeRate;
            }

            if (Input.GetKey(KeyCode.D) && transform.position.y <= grapplePos.y + 2)
            {
                player.AddForce(-transform.right * force);
            }

            if (Input.GetKey(KeyCode.A) && transform.position.y <= grapplePos.y + 2)
            {
                player.AddForce(transform.right * force);
            }

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Vector2.SignedAngle(Vector2.up, player.position - grapplePos)));
        }

        if (pull)
        {
            player.AddForce(pullDirection * pullForce);
        }

        player.velocity = Vector2.ClampMagnitude(player.velocity, maxVelocity);
    }

    public void Swing(GameObject grapplePoint)
    {
        if (!swing && !pull)
        {
            soundEffect.Play();
            swing = true;
            grappleFrame = true;
            joint.enabled = true;
            joint.connectedBody = grapplePoint.GetComponent<Rigidbody2D>();
            activePoint = grapplePoint;
            grapplePos = grapplePoint.transform.position;
            joint.distance = Vector2.Distance(transform.position, grapplePos);
        }     
        
        if (firstGrapple)
        {
            player.isKinematic = false;
            firstGrapple = false;
        }
    }

    public void Pull(GameObject grapplePoint)
    {
        if (!swing)
        {
            soundEffect.Play();
            player.velocity = new Vector2(0, 0);
            pull = true;
            pullTimer = 0;
            activePoint = grapplePoint;
            grapplePos = grapplePoint.transform.position;
            pullDirection = grapplePos - player.position;
            grappleFrame = true;
        }

        if (firstGrapple)
        {
            player.isKinematic = false;
            firstGrapple = false;
        }
    }

    public void Ungrapple()
    {
        if (activePoint != null)
        {
            activePoint.GetComponent<GrapplePoint>().grappled = false;
        }
        activePoint = null;

        if (pull)
        {
            pull = false;
            pullTimer = 0;
        }

        if (swing)
        {
            swing = false;
            joint.enabled = false;
        }

        grappled = false;      
        grappleLine.SetPositions(new Vector3[] { transform.position, transform.position });
    }
}
