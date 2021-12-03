using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float tileSpeed;
    public float tileLength;
    public int numberOfTiles;
    public float zSpawn;
    public float height;

    private float oldSpeed;
    private Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();
    private Vector3 _resetPos;
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

        _resetPos = new Vector3(0, 0, zSpawn);
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

        for (int i = 0; i < activeTiles.Count; i++)
        {

            if (activeTiles[i].gameObject.transform.position.z < playerTransform.position.z - (tileLength / 2) - 1f)
            {
                activeTiles[i].transform.position = GetLastTilePosition(i);
            }

        }
    }

    public Vector3 GetLastTilePosition(int index)
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = activeTiles.Count - 1;
        }

        Vector3 lastTilePos = activeTiles[index].transform.position;
        lastTilePos.z += tileLength;
        return lastTilePos;
    }
    public void SpawnTile(int index)
    {
        GameObject go = Instantiate(tilePrefabs[index], new Vector3(0, height, 1 * zSpawn), transform.rotation);
        go.transform.parent = this.gameObject.transform;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
