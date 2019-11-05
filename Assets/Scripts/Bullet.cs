using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Actor") {
            Actor targetHit = other.gameObject.GetComponent(typeof(Actor)) as Actor;
            if (gameObject.name == "Bullet(Clone)" && other.name != "Player" || gameObject.name == "EnemyBullet(Clone)" && other.name == "Player" || other.name == "Wall") {
                if (targetHit != null)
                    targetHit.Damage(damage);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Bullet"))
        {
            if (transform.localScale.x + transform.localScale.y <= other.transform.localScale.x + other.transform.localScale.y)
            {
                Destroy(gameObject);
                return;
            }
            return;
        }
    }
}
