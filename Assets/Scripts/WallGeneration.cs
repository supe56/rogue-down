using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    public GameObject wall;
    public float moveDistance = 2f;
    private GameObject[] floors;
    private GameObject[] enemies;
    public GameObject player;
    private float topX;
    private float minX;
    private float topY;
    private float minY;
    void Awake()
    {
        floors = GameObject.FindGameObjectsWithTag("Floor");
        for (int i = 0; i < floors.Length; i++) {                       // Find corner floors
            if (floors[i].transform.position.x > topX)
                topX = floors[i].transform.position.x;
            if (floors[i].transform.position.x < minX)
                minX = floors[i].transform.position.x;
            if (floors[i].transform.position.y > topY)
                topY = floors[i].transform.position.y;
            if (floors[i].transform.position.y < minY)
                minY = floors[i].transform.position.y;
        }

        topX += moveDistance; // Outer walls
        minX -= moveDistance;
        topY += moveDistance;
        minY -= moveDistance;

        transform.position = new Vector2(minX, minY);                   //Move to bottom left

        for (float i = minY; i < topY; i++) {                                                       //Instantiate walls
            for (float o = minX; o < topX; o++) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f);
                if (hit.collider == null)
                    Instantiate(wall, transform.position, Quaternion.identity);
                if (transform.position.x < topX)
                    transform.position += Vector3.right * moveDistance;
                else
                    transform.position += Vector3.right * (minX - topX);
            }
         transform.position += Vector3.up * moveDistance;
        }
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }
}