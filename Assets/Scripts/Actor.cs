using UnityEngine;
namespace Players
{
    public class Actor : MonoBehaviour
    {

        public float moveSpeed = 7f;
        public float tpRange = 5;
        public float tpCooldown = 2.5f;
        public float bulletSpeed = 15f;
        public float fireRate = 0.75f;
        public float health = 100;
        public float startHealth = 100;

        public GameObject gun;
        public GameObject bullet;

        public float vertical;
        public float horizontal;

        bool canFire = true;
        bool canTp = true;
        readonly float moveLimiter = 0.7f;

        public float GetAngle(string dest) // Returns the angle between the mouse and the center of the screen
        {
            if (dest == "Mouse")
            {
                Vector2 mousePos;
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
                lookPos -= transform.position;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                return angle;
            }
            else
            {
                Vector3 objectPos = GameObject.Find(dest).transform.position;
                objectPos -= transform.position;
                float angle = Mathf.Atan2(-objectPos.x, objectPos.y) * Mathf.Rad2Deg;
                return angle;
            }
        }

        public void Dash(float range)
        {
            if (!canTp) return;
            float angle = GetAngle("Mouse") * Mathf.Deg2Rad;
            Vector2 dashPos;
            dashPos.x = Mathf.Cos(angle);
            dashPos.y = Mathf.Sin(angle);
            transform.Translate(dashPos * range);
            canTp = false;
            Invoke("PrepareTp", tpCooldown);
        }

        void PrepareTp() { canTp = true; }
        void PrepareFire() { canFire = true; }

        public void Fire()
        {
            if (!canFire) return;
            GameObject newBullet;
            newBullet = Instantiate(bullet, transform.position, gun.transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = (gun.transform.up * bulletSpeed);
            canFire = false;
            Invoke("PrepareFire", fireRate);
        }

        public void Move(float horizontal, float vertical)
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }

        public void Damage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}