using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Transform target;
    Transform ballistaTop;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem projectileParticles;

    // Start is called before the first frame update
    void Start()
    {
        ballistaTop = this.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);
        ballistaTop.transform.LookAt(target);

        if(targetDistance < towerRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emmisionComponent = projectileParticles.emission;
        emmisionComponent.enabled = isActive;
    }
}
