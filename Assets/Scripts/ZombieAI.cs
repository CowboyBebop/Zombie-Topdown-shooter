using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZombieAI : Unit
{
    [SerializeField] float chaseOffset;
    [SerializeField] float moveSpeed;
    private Vector2 vectorToPlayer;
    private Vector2 playerPos;
    private Transform playerTransform;
    private float rotationAngle;
    [SerializeField] private float knockbackForce;

    private void Awake()
    {
        noKnockbackCooldown = true;
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerTransform.position;

        vectorToPlayer = (playerPos - (Vector2)transform.position);
        movementDirection = vectorToPlayer.normalized;

        rotationAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        rb.rotation = rotationAngle;

        movementVelocity = movementDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (vectorToPlayer.magnitude >= chaseOffset)
        {
            rb.velocity = movementVelocity + additionalVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.ReceiveKnockBackFromPosition(transform.position, knockbackForce);
        }
    }
}
