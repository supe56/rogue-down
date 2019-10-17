using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.tag == "Actor")
        {
            if (gameObject.name == "Bullet(Clone)" && other.name != "Player" || gameObject.name == "EnemyBullet(Clone)" && other.name == "Player")
            {
                Actor targetHit = other.gameObject.GetComponent(typeof(Actor)) as Actor;
                if (targetHit != null) targetHit.Damage(damage);
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
