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
            awake = false;
        }

        void Update()
        {
            if (awake == true) {
                gun.transform.rotation = Quaternion.AngleAxis(GetAngle("Player"), Vector3.forward);
                Fire();
                if (Vector3.Distance(transform.position, player.transform.position) > enemyDistance) {
                    if (transform.position.x - player.transform.position.x < -0.1f) horizontal = 1; //right
                    else if (transform.position.x - player.transform.position.x > 0.1f) horizontal = -1; //left
                    else horizontal = 0;
                    if (transform.position.y - player.transform.position.y < -0.1f) vertical = 1; //up
                    else if (transform.position.y - player.transform.position.y > 0.1f) vertical = -1; //down
                    else vertical = 0;
                    Move(horizontal, vertical);
                }
            }
            else if (GameObject.Find("Player") != null) {
                if (player == null) 
                    player = GameObject.Find("Player");
                if(Vector3.Distance(transform.position, player.transform.position) < wakeRange) {
                    awake = true;
                }
                Debug.Log("asd");
            }

        }
    }

}
