using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Player player;
    private Transform shootingPoint;
    private Transform pivotRotate;

    private Rigidbody2D bullet_rb2d;
    private Vector3 moveDirection;

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
    private void Awake()
    {
        bullet_rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        shootingPoint = player.GetComponent<PlayerControl>().shootingPoint;
        pivotRotate = player.GetComponent<PlayerControl>().pivotRotate;

        Vector3 _randomDir = new Vector3(shootingPoint.position.x, shootingPoint.position.y + Random.Range(-0.05f, 0.05f), shootingPoint.position.z);
        moveDirection = (_randomDir - pivotRotate.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}