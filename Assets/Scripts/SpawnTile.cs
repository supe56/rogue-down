using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tile;

    void Start()
    {
        if (!Physics2D.Raycast(transform.position + new Vector3(0, 0, -1), Vector3.forward))
            Instantiate(tile, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
