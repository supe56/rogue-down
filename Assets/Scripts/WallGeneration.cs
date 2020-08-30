using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    public GameObject wall;
    public float moveDistance = 2f;
    public GameObject player;
    private float topX;
    private float minX;
    private float topY;
    private float minY;

    void Awake()
    {
        var allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; // this will grab all GameObjects from the current scene!
        foreach (GameObject obj in allObjects) {
            if (obj.tag == "Floor") {
                if (obj.transform.position.y > topY)
                    topY = obj.transform.position.y;
                if (obj.transform.position.x < minX)
                    minX = obj.transform.position.x;
                if (obj.transform.position.y < minY)
                    minY = obj.transform.position.y;
                if (obj.transform.position.x > topX)
                    topX = obj.transform.position.x;
            }
        }
        topX += 4; // Outer walls
        minX -= 4;
        topY += 4;
        minY -= 4;

        transform.position = new Vector2(minX, minY);                   //Move to bottom left
    }

    private void FixedUpdate()
    {
        int mask = (1 << 9);                                            //dunno what this does lmao
        RaycastHit2D bigHit = Physics2D.CircleCast(transform.position + new Vector3(0, 0, -1), 1.5f, Vector3.forward, Mathf.Infinity, mask);
        RaycastHit2D smallHit = Physics2D.CircleCast(transform.position + new Vector3(0, 0, -1), 0.1f, Vector3.forward);
        if (bigHit.collider != null) {
            if (bigHit.collider.tag == "Floor" && smallHit.collider == null) {
                Instantiate(wall, transform.position, Quaternion.identity);
            }
        }
        if (transform.position.x < topX)
            transform.position += Vector3.right * moveDistance;
        else
            transform.position += Vector3.right * (minX - topX);

        if(transform.position.x >= topX)
            transform.position += Vector3.up * moveDistance;

        if (transform.position.y >= topY) {
            Time.timeScale = 1;
            Instantiate(player, Vector3.zero, Quaternion.identity);
            Destroy(this);
        }
    }
}