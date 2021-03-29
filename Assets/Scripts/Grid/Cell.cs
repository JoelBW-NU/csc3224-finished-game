using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    [HideInInspector]
    public Grid grid;

    BoxCollider2D cellCollider;
    public float grapplePointDist = 0.5f;

    [HideInInspector]
    public GameObject grapplePointPrefab;

    int RADIUS_LAYER_ID = 8;

    List<GrapplePoint> grapplePoints;

    public void Initialise()
    {
        cellCollider = GetComponent<BoxCollider2D>();
        grapplePoints = new List<GrapplePoint>();
        Vector3 newScale = transform.localScale;
        newScale.Set(newScale.x * grid.scaleX, newScale.y * grid.scaleY, 1);
        transform.localScale = newScale;
        foreach (CellEdge edge in GetComponentsInChildren<CellEdge>())
        {
            edge.grid = grid;
        }
    }

    void Populate(int numPoints)
    {
        int pointsPlaced = 0;

        while (pointsPlaced < numPoints)
        {           
            Vector2 position = new Vector2(Random.Range(cellCollider.bounds.min.x, cellCollider.bounds.max.x), Random.Range(cellCollider.bounds.min.y, cellCollider.bounds.max.y));
            Collider2D col = Physics2D.OverlapCircle(position, 2, 1 << RADIUS_LAYER_ID);

            if (col == null || !col.CompareTag("Grapple Point Outer"))
            {
                GrapplePoint grapplePoint = Instantiate(grapplePointPrefab, position, Quaternion.identity).GetComponent<GrapplePoint>();
                grapplePoint.transform.parent = transform;
                grapplePoint.grapple = grid.playerGrapple;
                grapplePoints.Add(grapplePoint);
                ++pointsPlaced;
            }
        }
    }

    void Depopulate()
    {
        foreach (GrapplePoint grapplePoint in grapplePoints)
        {
            if (!grapplePoint.countdownBegun)
            {
                grapplePoint.DestroyPoint();
            }
        }
        grapplePoints.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Screen Bounds"))
        {
            Populate(grid.numPointsPerCell);
        }
        else if (col.gameObject.CompareTag("Player")) 
        {
            grid.NewCenter(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Screen Bounds"))
        {
            Depopulate();
            grid.RemoveCell(transform.position);
            Destroy(gameObject);
        }
    }
}