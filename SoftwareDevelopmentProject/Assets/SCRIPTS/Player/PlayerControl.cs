using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;

    private Rigidbody2D player_rb2d;
    private SpriteRenderer player_sr;

    private Vector2 movementVector;

    //======================================================================
    private void Awake()
    {
        player_rb2d = GetComponent<Rigidbody2D>();
        player_sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
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
}
