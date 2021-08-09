using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways] // This tag will make sure that the script is executed even in the Scene View.
public class CoordinateLabler : MonoBehaviour
{
    // Serializing these color variables is not updating all the tiles in the game for some reason
    Color defaultColor = Color.white;
    Color blockedColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = new Color(1f, 0.5f, 0f); // orange color

    TextMeshPro label;
    public Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = this.GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }

    // Start is called before the first frame update
    private void Start()
    {
        label.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            label.enabled = true;
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();
        ToggleLables();
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        /* Since the snapping is in the order of 10s, we need to divide
           the resultant coordinates by 10                              */

        coordinates.x = (Mathf.RoundToInt(transform.parent.position.x)) / gridManager.UnitySnapSettings;
        coordinates.y = (Mathf.RoundToInt(transform.parent.position.z))/ gridManager.UnitySnapSettings;

        label.text = coordinates.ToString();
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor; // gray
        }
        else if (node.isPath)
        {
            label.color = pathColor; // orange
        }
        else if (node.isExplored)
        {
            label.color = exploredColor; // yellow
        }
        else
        {
            label.color = defaultColor; // white
        }
    }

    void ToggleLables()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
