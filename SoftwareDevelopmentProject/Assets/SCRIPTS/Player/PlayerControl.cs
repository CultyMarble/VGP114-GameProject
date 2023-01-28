using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;

    private Rigidbody2D player_rb2d;
    private SpriteRenderer player_sr;
    private Animator player_animator;

    private Vector2 movementVector;

    [SerializeField] private GameObject pf_bullet;
    [SerializeField] private float fireRate = 0.075f;
    public Transform shootingPoint;
    public Transform pivotRotate;
    private bool isShooting;
    private float fireRateCounter;
    private Vector3 aimDirection;

    //======================================================================
    private void Awake()
    {
        player_rb2d = GetComponent<Rigidbody2D>();
        player_sr = GetComponentInChildren<SpriteRenderer>();
        player_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        UpdatePlayerAnimator();
        PlayerShootingHandler();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        FlipSprite();
    }

    //======================================================================
    private void MovePlayer()
    {
        player_rb2d.MovePosition(player_rb2d.position + movementVector.normalized * movementSpeed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (movementVector.x < 0)
            player_sr.flipX = true;
        else if (movementVector.x > 0)
            player_sr.flipX = false;
    }

    private void UpdatePlayerAnimator()
    {
        if (movementVector != Vector2.zero)
            player_animator.SetBool("isRunning", true);
        else
            player_animator.SetBool("isRunning", false);
    }

    private void PlayerShootingHandler()
    {
        fireRateCounter += Time.deltaTime;

        // Update Pivot Direction
        aimDirection = (CultyMarbleHelper.GetMouseToWorldPosition() - pivotRotate.position).normalized;
        pivotRotate.transform.eulerAngles = new Vector3(0.0f, 0.0f, CultyMarbleHelper.GetAngleFromVector(aimDirection));

        if (Input.GetMouseButtonDown(0))
        {
            if (fireRateCounter > fireRate) fireRateCounter = fireRate;
            isShooting = true;
        }

        if (Input.GetMouseButton(0) && isShooting)
        {
            if (fireRateCounter >= fireRate)
            {
                fireRateCounter -= fireRate;

                // Create bullet
                Transform bullet_tranform = Instantiate(pf_bullet, shootingPoint.position, transform.rotation).transform;
                Destroy(bullet_tranform.gameObject, 3.0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
            isShooting = false;
    }
}
