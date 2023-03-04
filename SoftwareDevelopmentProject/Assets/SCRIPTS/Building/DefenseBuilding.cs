using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuilding : MonoBehaviour
{
    [SerializeField] private Transform firingPosition;
    [SerializeField] private Transform towerHead;

    private Enemy currentTarget;

    private float lookForTargetTimeCounter;
    private float lookForTargetTime = 0.5f;

    private float towerRange = 20f;
    private float fireRateCounter;
    [SerializeField] private float fireRate;

    //===========================================================================
    private void Start()
    {
        fireRateCounter = fireRate;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    //===========================================================================
    private void LookForTarget()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerRange);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();

            if (enemy != null)
            {// is an enemy
                if (currentTarget == null)
                {
                    currentTarget = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, currentTarget.transform.position))
                        currentTarget = enemy;
                }
            }
        }
    }

    private void HandleTargeting()
    {
        if (currentTarget == null)
        {
            lookForTargetTimeCounter -= Time.deltaTime;
            if (lookForTargetTimeCounter <= 0.0f)
            {
                lookForTargetTimeCounter += lookForTargetTime;
                LookForTarget();
            }
        }
    }

    private void HandleShooting()
    {
        if (currentTarget == null) return;

        fireRateCounter -= Time.deltaTime;
        if (fireRateCounter <= 0)
        {
            fireRateCounter += fireRate;
            GunnerBullet.Spawn(firingPosition.position, currentTarget);
        }
    }
}