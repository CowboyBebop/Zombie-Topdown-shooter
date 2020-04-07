using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    float mouseAngle;
    Vector2 moveDir,mouseDir;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseDir = ((Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;

        moveDir = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };

        moveDir.Normalize();

        mouseAngle = Mathf.Atan2(mouseDir.y,mouseDir.x) * Mathf.Rad2Deg - 180;

        rb.rotation = mouseAngle;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed * Time.fixedDeltaTime;
    }
}
