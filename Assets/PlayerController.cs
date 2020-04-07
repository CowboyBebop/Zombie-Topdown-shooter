using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private float mouseAngle;
    private Vector2 moveDir,mouseDir,mousePos;
    private Rigidbody2D rb;

    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform gunPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = (mousePos - (Vector2)transform.position).normalized;

        moveDir = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };

        moveDir.Normalize();

        mouseAngle = Mathf.Atan2(mouseDir.y,mouseDir.x) * Mathf.Rad2Deg;

        rb.rotation = mouseAngle;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed * Time.fixedDeltaTime;
    }


    private void Shoot()
    {
        ShakeCamera(.1f, .05f);
        GameObject bullet = Instantiate(BulletPrefab, gunPoint.position,Quaternion.identity);
        bullet.GetComponent<Bullet>().moveDir = mouseDir;
    }

    public static void ShakeCamera(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate () {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
    }
}
