using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Player : Actor
    {
        // Start is called before the first frame update
        private void Start()
        {
            moveSpeed = 7f;
            tpRange = 5f;
            tpCooldown = 2.5f;
            bulletSpeed = 15f;
            fireRate = 0.2f;
            health = startHealth;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) Dash(tpRange);
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            Move(horizontal, vertical);
            gun.transform.rotation = Quaternion.AngleAxis(GetAngle("Mouse") - 90, Vector3.forward);
        }
    }

}