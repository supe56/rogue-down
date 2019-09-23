using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 7f;
    public float tpRange = 5;
    public float tpCooldown = 2.5f;

    public float bulletSpread = 0.5f;
    public float bulletSpeed = 15f;

    public GameObject gun;
    public GameObject bullet;

    bool canTp = true;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Mouse1)) Dash(tpRange);
        gun.transform.rotation = Quaternion.AngleAxis(GetAngle() - 90, Vector3.forward);
        if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
    }

    float GetAngle() // Returns the angle between the mouse and the center of the screen
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        return angle;
    }

    void Dash(float range)
    {
        if (!canTp) return;
        float angle = GetAngle() * Mathf.Deg2Rad;
        Vector2 dashPos;
        dashPos.x = Mathf.Cos(angle);
        dashPos.y = Mathf.Sin(angle);
        transform.Translate(dashPos * range);
        canTp = false;
        Invoke("PrepareTp", tpCooldown);
    }

    void PrepareTp() { canTp = true; }

    void Fire()
    {
        GameObject newBullet;
        Vector3 bulletSpreadVector = new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
        newBullet = Instantiate(bullet, transform.position, gun.transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = (gun.transform.position * bulletSpeed) + bulletSpreadVector;
    }
}