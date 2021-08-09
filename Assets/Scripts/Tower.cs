using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float buildDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BuildTower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CreateTower(Tower towerPrefab, Vector3 tilePos)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }
        if(bank.CurrentBalance >= cost)
        {
            Instantiate(towerPrefab.gameObject, tilePos, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;
    }

    IEnumerator BuildTower()
    {
        // switch off all children and grandchildren in the hierarchy
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false); 
            }
        }

        // switch on all children and grandchildren in the hierarchy with a buildDelay
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
