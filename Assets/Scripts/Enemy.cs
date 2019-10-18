using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Enemy : Actor
    {
        public GameObject player;
        public float enemyDistance = 7f;
        private void Start()
        {
            health = startHealth;
            moveSpeed = 3.5f;
            player = GameObject.Find("Player");
        }
        void Update()
        {
            gun.transform.rotation = Quaternion.AngleAxis(GetAngle("Player"), Vector3.forward);
            Fire();
            if (transform.position.x - player.transform.position.x < -enemyDistance) horizontal = 1;
            else if (transform.position.x - player.transform.position.x > enemyDistance) horizontal = -1;
            else horizontal = 0;
            if (transform.position.y - player.transform.position.y < -enemyDistance) vertical = 1;
            else if (transform.position.y - player.transform.position.y > enemyDistance) vertical = -1;
            else vertical = 0;
            Move(horizontal, vertical);
        }
    }

}
