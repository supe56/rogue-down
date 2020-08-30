using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Enemy : Actor
    {
        public GameObject player;
        public float enemyDistance = 7f;
        public LayerMask IgnoreMe;

        bool moveQueue = false;

        private void Awake()
        {
            health = startHealth;
            moveSpeed = 3.5f;
            awake = false;
        }

        void Update()
        {
            if (awake == true) {
                gun.transform.rotation = Quaternion.AngleAxis(GetAngle(player.name), Vector3.forward);                                                  //Rotate gun + shoot
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position, wakeRange, ~IgnoreMe);  //Find line of sight
                for (int i = 0; i <= hit.Length; i++) {
                    if (hit[i].collider == null)
                        return;
                    string name = hit[i].collider.name;
                    if (name != "Player(Clone)" && name != "Enemy(Clone)")
                        return;
                    else if (name == "Player(Clone)") {
                        Fire();                                                                                                                         //fire
                        i = hit.Length + 1;                                                                             
                    }
                }

                if (Vector3.Distance(transform.position, player.transform.position) > enemyDistance) {                          //Move
                    if (transform.position.x - player.transform.position.x < -0.1f) horizontal = 1; //right
                    else if (transform.position.x - player.transform.position.x > 0.1f) horizontal = -1; //left
                    else horizontal = 0;
                    if (transform.position.y - player.transform.position.y < -0.1f) vertical = 1; //up
                    else if (transform.position.y - player.transform.position.y > 0.1f) vertical = -1; //down
                    else vertical = 0;
                }
                else if (!moveQueue) {
                    Debug.Log("1");
                    horizontal = 0;
                    vertical = 0;
                    Invoke("RandomMove", Random.Range(2, 3.5f));
                    moveQueue = true;
                }
                Move(horizontal, vertical);
            }

            else if (GameObject.Find("Player(Clone)") != null) {                                                                        //Wake up
                player = GameObject.Find("Player(Clone)");

                if (Vector3.Distance(transform.position, player.transform.position) < wakeRange) {
                    RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position, wakeRange, ~IgnoreMe);
                    for (int i = 0; i <= hit.Length; i++) {
                        if (hit[i].collider == null)
                            return;
                        string name = hit[i].collider.name;
                        if (name != "Player(Clone)" && name != "Enemy(Clone)")
                            return;
                        else if (name == "Player(Clone)") {
                            awake = true;
                            i = hit.Length + 1;
                        }
                    }
                }
            }

        }

        void RandomMove()
        {
            Debug.Log("2");
            horizontal = Random.Range(-1f, 1f);
            vertical = Random.Range(-1f, 1f);

            Invoke("StopMove", Random.Range(0.5f, 2));
        }

        void StopMove()
        {
            Debug.Log("3");
            horizontal = 0;
            vertical = 0;
            moveQueue = false;
        }
    }
}
