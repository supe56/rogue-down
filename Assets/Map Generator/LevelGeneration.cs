using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private int direction; // 1UP 2RIGHT 3DOWN 4LEFT
    public float btwMove = 0.02f;
    public float moveAmount = 1f;
    public GameObject floorTile;
    private void Start() {
        direction = Random.Range(1, 5);
        Instantiate(floorTile, transform.position, Quaternion.identity);
        Move();
    }

    private void Move() {
        if (direction == 1) transform.position += Vector3.up * moveAmount;
        if (direction == 2) transform.position += Vector3.right * moveAmount;
        if (direction == 3) transform.position += Vector3.down * moveAmount;
        if (direction == 4) transform.position += Vector3.left * moveAmount;
        findNewDir(direction);
        Instantiate(floorTile, transform.position, Quaternion.identity);
        Invoke("Move", btwMove);
    }

    private void findNewDir(int oldDir) {
        direction = Random.Range(1, 5);
        if(direction == oldDir) findNewDir(oldDir);
    }
}
