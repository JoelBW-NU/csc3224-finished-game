using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEdge : MonoBehaviour
{
    enum Edge { TOP, RIGHT, LEFT, BOTTOM };

    [SerializeField]
    Edge edge;

    [HideInInspector]
    public Grid grid;

    public bool colliderActive = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Screen Bounds") && colliderActive)
        {
            if (edge == Edge.TOP)
            {
                grid.NewCell(Grid.CellPosition.TOP, transform.parent.position);
            } 
            else if (edge == Edge.BOTTOM)
            {
                grid.NewCell(Grid.CellPosition.BOTTOM, transform.parent.position);
            }
            else if (edge == Edge.LEFT)
            {
                grid.NewCell(Grid.CellPosition.LEFT, transform.parent.position);
            }
            else if (edge == Edge.RIGHT)
            {
                grid.NewCell(Grid.CellPosition.RIGHT, transform.parent.position);
            }
        }

        foreach (CellEdge edge in transform.parent.GetComponentsInChildren<CellEdge>())
        {
            edge.colliderActive = true;
        }
        colliderActive = true;
    }
}
