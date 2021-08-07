using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways] // This tag will make sure that the script is executed even in the Scene View.
public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    public Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;
    
    void Awake()
    {
        label = this.GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
        label.enabled = true;
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
        /* Since the snapping is in the order of 10s, we need to divide
           the resultant coordinates by 10                              */
        int snapping = Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.x);
        coordinates.x = (Mathf.RoundToInt(transform.parent.position.x))/snapping;
        coordinates.y = (Mathf.RoundToInt(transform.parent.position.z))/snapping;

        label.text = coordinates.ToString();
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
