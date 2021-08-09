using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Mouse inputs will be written in this script because this script is attached to the tiles 
   and we need to access the name of the tiles, when we click on the respective tile.       */
public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            Vector3 tilePos = this.transform.position;
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, tilePos);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyRecievers();
            }
        }
    }
}
