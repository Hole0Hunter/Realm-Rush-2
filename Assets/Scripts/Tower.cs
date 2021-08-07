using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
