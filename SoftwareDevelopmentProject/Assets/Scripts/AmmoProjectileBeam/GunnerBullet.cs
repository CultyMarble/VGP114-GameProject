using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBullet : MonoBehaviour
{
    public static GunnerBullet Spawn(Vector3 positition, Enemy targetEnemy)
    {
        Transform pfbullet = Resources.Load<Transform>("gunner_bullet");
        Transform bulletTransform = Instantiate(pfbullet, positition, Quaternion.identity);

        GunnerBullet bullet = bulletTransform.GetComponent<GunnerBullet>();
        bullet.SetTarget(targetEnemy);

        return bullet;
    }

    //===========================================================================
    private Enemy targetEnemy;
    private Vector3 moveDirection;
    private float moveSpeed = 20.0f;

    private Vector3 lastMoveDir;
    private float lifeTime = 2.0f;

    bool isHit = true;

    //===========================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isHit == true)
            {
                collision.GetComponent<HealthSystem>().ReduceCurrentHealth(5);
                isHit = false;
            }

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //===========================================================================
    private void Update()
    {
        if (targetEnemy != null)
        {
            moveDirection = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDirection;
        }
        else
        {
            moveDirection = lastMoveDir;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0.0f, 0.0f, CultyMarbleHelper.GetAngleFromVector(moveDirection));

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }

    private void SetTarget (Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }
}