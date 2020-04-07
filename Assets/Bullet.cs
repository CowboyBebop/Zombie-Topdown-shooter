using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 50;
    private Rigidbody2D rb;
    [SerializeField] public Vector2 moveDir;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        rb.velocity = moveDir * bulletSpeed;

        Destroy(gameObject, 2f);
    }
}
    
