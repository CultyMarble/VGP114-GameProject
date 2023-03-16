using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float knockBackDistance;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int experience;
    [SerializeField] private int metalDropMin;
    [SerializeField] private int metalDropMax;

    private PlayerLevelSystem playerLevelSystem;
    private Transform currentTargetTransform;
    private Rigidbody2D enemy_rb2D;
    private Vector2 movingDirection;

    private float lookForTargetTimeCounter;
    private float lookForTargetTimeMin = 0.5f;
    private float lookForTargetTimeMax = 1.5f;

    private HealthSystem buildingHealth;
    private HealthSystem healthSystem;
    //===========================================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collisionTransform = collision.transform;
        Attack(collisionTransform);
    }

    //===========================================================================
    private void Awake()
    {
        enemy_rb2D = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();

        playerLevelSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevelSystem>();
    }

    private void Start()
    {
        currentTargetTransform = FindObjectOfType<Player>().transform;
        lookForTargetTimeCounter = Random.Range(lookForTargetTimeMin, lookForTargetTimeMax);

        healthSystem.OnDestroy += Enemy_OnDestroyHandler;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void OnDestroy()
    {
        healthSystem.OnDestroy -= Enemy_OnDestroyHandler;
        healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }

    private void FixedUpdate()
    {
        HandleTargeting();
        MoveTowardCurrentTarget();
        FlipSprite();
    }

    //===========================================================================
    private void Attack(Transform collisionTransform)
    {
        if (collisionTransform.GetComponent<Player>() != null)
        {
            collisionTransform.GetComponent<PlayerHealth>().DecreaseCurrentHealth(10);
            KnockBack(collisionTransform);
        }

        if (collisionTransform.GetComponent<Building>() != null)
        {
            collisionTransform.GetComponent<HealthSystem>().ReduceCurrentHealth(5);
            KnockBack(collisionTransform, 0.1f);
        }
    }

    private void KnockBack(Transform collisionTransform, float knockForce = 1.0f)
    {
        // Calculate knocking direction based on Collision and Object Position
        Vector2 knockDirection = -(collisionTransform.position - transform.position).normalized;

        // Add knockForce
        enemy_rb2D.AddForce(knockDirection * knockForce * knockBackDistance * Time.deltaTime);
    }

    private void LookForTarget()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();

            if (building != null)
            {// is a building
                if (currentTargetTransform == null || currentTargetTransform == FindObjectOfType<Player>().transform)
                {
                    currentTargetTransform = building.transform;
                    return;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, currentTargetTransform.position))
                        currentTargetTransform = building.transform;
                    return;
                }
            }
        }

        currentTargetTransform = FindObjectOfType<Player>().transform;
    }

    private void HandleTargeting()
    {
        if (currentTargetTransform == null)
        {
            movingDirection = new Vector2(0.0f, 0.0f);
            LookForTarget();
        }
        else
        {
            lookForTargetTimeCounter -= Time.deltaTime;
            if (lookForTargetTimeCounter <= 0.0f)
            {
                lookForTargetTimeCounter += lookForTargetTimeMax;
                LookForTarget();
            }
        }
    }

    private void MoveTowardCurrentTarget()
    {
        if (currentTargetTransform == null) return;

        movingDirection = (currentTargetTransform.transform.position - transform.position).normalized;
        enemy_rb2D.velocity = movingDirection * speed;
    }

    private void Enemy_OnDestroyHandler(object sender, System.EventArgs e)
    {
        SoundEffectManager.Instance.PlaySound(SoundEffectManager.EnumSound.EnemyDie);
        playerLevelSystem.IncreaseExp(experience);
        ResourceManager.Instance.AddResource(CurrencyType.Metal, UnityEngine.Random.Range(metalDropMin, metalDropMax));
        Destroy(gameObject);
    }

    private void HealthSystem_OnHealthChanged(object sender, HealthSystem.OnHealthChangedEvenArgs e)
    {
        SoundEffectManager.Instance.PlaySound(SoundEffectManager.EnumSound.EnemyHit);
    }

    private void FlipSprite()
    {
        if (currentTargetTransform.position.x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}