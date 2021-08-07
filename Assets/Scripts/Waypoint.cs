using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Mouse inputs will be written in this script because this script is attached to the tiles 
   and we need to access the name of the tiles, when we click on the respective tile.       */
public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {get { return isPlaceable; } }
    
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            Vector3 tilePos = this.transform.position;
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, tilePos);
            isPlaceable = !isPlaced;
        }
    }
}
