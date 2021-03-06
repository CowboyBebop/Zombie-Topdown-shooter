﻿using CodeMonkey.Utils;
using System.Threading;
using UnityEngine;

public class PlayerController : Unit
{

    [SerializeField] private float moveSpeed;
    private float mouseAngle;
    private Vector2 moveDir, mouseDir, mousePos;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform gunPoint;

    [SerializeField] private float fireRate = 10;
    private float lastFireTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 100;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = (mousePos - (Vector2)transform.position).normalized;

        moveDir = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };

        moveDir.Normalize();

        mouseAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        rb.rotation = mouseAngle;

        if (Input.GetMouseButton(0))
        {

            //WeaponHandler.Attack();
            if (Time.time - lastFireTime > 1 / fireRate)
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(moveDir * moveForce);
        if (moveDir.x != 0)
        {
            movementDirection.x = moveDir.x;
        }
        else
        {
            movementDirection.x = 0;
        }

        if (moveDir.y != 0)
        {
            movementDirection.y = moveDir.y;

        }
        else
        {
            movementDirection.y = 0;
        }

        movementVelocity = movementDirection * moveSpeed;

        rb.velocity = movementVelocity + additionalVelocity;
    }

    private void Shoot()
    {
        ShakeCamera(.1f, .05f);
        GameObject bullet = Instantiate(BulletPrefab, gunPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().moveDir = mouseDir;
    }

    public static void ShakeCamera(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate ()
        {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
    }
}