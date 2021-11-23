using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float tileSpeed;
    public float tileLength;
    public int numberOfTiles;
    public float zSpawn;

    private float oldSpeed;
    private Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
                zSpawn += tileLength;
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
                zSpawn += tileLength;
            }
        }

        playerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        Tile.speed = tileSpeed;
        oldSpeed = tileSpeed;
    }

    void Update()
    {
        if(oldSpeed != tileSpeed)
        {
            oldSpeed = tileSpeed;
            Tile.speed = tileSpeed;
        }

        if(activeTiles[0].gameObject.transform.position.z < playerTransform.position.z - tileLength)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }          
    }

    public void SpawnTile(int index)
    {
        GameObject go = Instantiate(tilePrefabs[index], new Vector3(0, 0, 1 * zSpawn), transform.rotation);
        go.transform.parent = this.gameObject.transform;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
