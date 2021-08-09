using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
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
        StartCoroutine(PrintTilessList());
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        
        foreach(Transform child in parent.GetComponentsInChildren<Transform>())
        {
            Tile tile = child.GetComponent<Tile>();

            if(tile != null)
            {
                path.Add(tile);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintTilessList()
    {
        foreach(Tile tile in path)
        {
            Vector3 startPos = this.transform.position;
            Vector3 endPos = tile.transform.position;
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
