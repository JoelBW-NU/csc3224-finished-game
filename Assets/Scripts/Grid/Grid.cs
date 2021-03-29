using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum CellPosition { TOP, BOTTOM, LEFT, RIGHT };

    [SerializeField]
    GameObject grapplePointPrefab;

    [SerializeField]
    GameObject cell;

    public Grapple playerGrapple;

    public int numPointsPerCell = 8;

    [HideInInspector]
    public float scaleX = 1;
    [HideInInspector]
    public float scaleY = 1;

    public float scaleMultiplier = 2f;

    Vector2 mainCellCenter;

    HashSet<Vector2> activeCellCoordinates;

    GameObject centerCell;
   

    // Start is called before the first frame update
    void Start()
    {
        scaleY = Camera.main.orthographicSize * 2 * scaleMultiplier;
        scaleX = (scaleY * Screen.width / Screen.height) * scaleMultiplier;
        mainCellCenter = new Vector2(0, 0);
        activeCellCoordinates = new HashSet<Vector2>();
        activeCellCoordinates.Add(mainCellCenter);
        centerCell = Instantiate(cell, mainCellCenter, Quaternion.identity);
        centerCell.transform.parent = gameObject.transform;
        centerCell.GetComponent<Cell>().grapplePointPrefab = grapplePointPrefab;
        centerCell.GetComponent<Cell>().grid = this;
        centerCell.GetComponent<Cell>().Initialise();
        foreach (CellEdge edge in centerCell.GetComponentsInChildren<CellEdge>())
        {
            edge.colliderActive = true;
        }
    }

    public bool NewCell(CellPosition position, Vector2 cellCenter)
    {
        Vector2 newCellCenter;

        switch (position)
        {
            case CellPosition.TOP:
                newCellCenter = new Vector2(cellCenter.x, cellCenter.y + scaleY);
                break;
            case CellPosition.BOTTOM:
                newCellCenter = new Vector2(cellCenter.x, cellCenter.y - scaleY);
                break;
            case CellPosition.LEFT:
                newCellCenter = new Vector2(cellCenter.x - scaleX, cellCenter.y);
                break;
            case CellPosition.RIGHT:
                newCellCenter = new Vector2(cellCenter.x + scaleX, cellCenter.y);
                break;
            default:
                newCellCenter = cellCenter;
                break;
        }

        if (cellExists(newCellCenter))
        {
            return false;
        }

        GameObject newCell = Instantiate(cell, newCellCenter, Quaternion.identity);
        activeCellCoordinates.Add(newCellCenter);
        newCell.transform.parent = gameObject.transform;
        Cell cellInfo = newCell.GetComponent<Cell>();
        cellInfo.grapplePointPrefab = grapplePointPrefab;
        cellInfo.grid = this;
        cellInfo.Initialise();
        return true;
    }

    public bool cellExists(Vector2 cellCoords)
    {
        return activeCellCoordinates.Contains(cellCoords);
    }

    public void RemoveCell(Vector2 cellCoords)
    {
        activeCellCoordinates.Remove(cellCoords);
    }

    public void NewCenter(GameObject cell)
    {
        centerCell = cell;
        mainCellCenter = cell.transform.position;
    }
}
