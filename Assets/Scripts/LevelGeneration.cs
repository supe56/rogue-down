using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [Range(0.0f, 100.0f)] public float turnChance;
    [Range(0.0f, 100.0f)] public float rightChance;
    [Range(0.0f, 100.0f)] public float branchChance;
    private GameObject lvlGen;
    [Range(0.0f, 100.0f)] public float roomChance;
    [Range(0.0f, 100.0f)] public float enemyChance;
    public GameObject[] rooms;

    public float totalFloors = 100;         //total number of floor tiles
    public float btwMove = 0.02f;           //between move
    public float moveDistance = 1f;
    public GameObject floorTile;
    public GameObject Enemy;
    public GameObject wallSpawner;

    private float deleteChance = 0f;

    private void Start()
    {
        Time.timeScale = 15;
        Instantiate(floorTile, transform.position, Quaternion.identity);
        Invoke("Move", btwMove);
    }

    private void Move()
    {
        if(Random.Range(0, 100) < turnChance) {                                 // Will it turn?
            if (Random.Range(0, 100) < rightChance) transform.Rotate(0, 0, 90); //Will it turn right
            else transform.Rotate(0, 0, -90);                                   //Or left?
        }
        transform.position += transform.right * moveDistance;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.9f);
        if (hit.collider == null) {
            Instantiate(floorTile, transform.position, Quaternion.identity);
            totalFloors--;
        }

        if (Random.Range(0, 100) < branchChance) CreateBranch();
        if (Random.Range(0, 100) < roomChance) GenerateRoom();
        if (Random.Range(0, 100) < enemyChance) SpawnEnemy();

        if (Random.Range(0, 100) < deleteChance) {
            SpawnItem();
            Destroy(gameObject);
        }
        if (totalFloors <= 0) {
            if (GameObject.FindGameObjectsWithTag("Level Gen").Length <= 1) {
                Instantiate(wallSpawner, new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),0), Quaternion.identity);
            }
            Destroy(gameObject);
        }

        Invoke("Move", btwMove);
    }

    private void GenerateRoom()
    {
       Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, transform.rotation);
    }

    private void CreateBranch()
    {
        totalFloors = Mathf.Floor(totalFloors / 2);
        deleteChance = GameObject.FindGameObjectsWithTag("Level Gen").Length * 3;
        lvlGen = gameObject;
        deleteChance = 0;
        Instantiate(lvlGen, transform.position, Quaternion.Euler(0, 0, transform.rotation.y + 90));
    }

    private void SpawnItem()
    {

    }

    private void SpawnEnemy() 
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
