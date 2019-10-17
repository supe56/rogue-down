using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Player : Actor
    {
        float horizontal;
        float vertical;
        // Start is called before the first frame update
        private new void Start()
        {
            moveSpeed = 7f;
            tpRange = 5f;
            tpCooldown = 2.5f;
            bulletSpeed = 15f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) Dash(tpRange);
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            Move(horizontal, vertical);
            gun.transform.rotation = Quaternion.AngleAxis(GetAngle() - 90, Vector3.forward);
        }
    }

}