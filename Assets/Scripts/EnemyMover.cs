using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)]float speed = 1f;

    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();    
    }
    void OnEnable()
    {
        FindPath();

        this.transform.position = path[0].transform.position; // returns to starting position
        StartCoroutine(PrintWaypointsList());
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        
        foreach(Transform child in parent.GetComponentsInChildren<Transform>())
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintWaypointsList()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPos = this.transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos); // for the enemy to look towards his end position;

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                this.transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        // enemy has moved till the end
        FinishPath();
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
