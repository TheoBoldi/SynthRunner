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
    private GameObject leftVolumeBar;
    private GameObject rightVolumeBar;
    private Vector3 leftEndPos;
    private Vector3 rightEndPos;
    private List<GameObject> leftSideCubes = new List<GameObject>();
    private List<GameObject> rightSideCubes = new List<GameObject>();
    private List<GameObject> activeTiles = new List<GameObject>();
    private Vector3 _resetPos;
    void Start()
    {
        leftVolumeBar = GameObject.Find("LeftVolumeBar").transform.GetChild(0).gameObject;
        rightVolumeBar = GameObject.Find("RightVolumeBar").transform.GetChild(0).gameObject;

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

        for(int i = 0; i < leftVolumeBar.transform.childCount; i++)
        {
            leftSideCubes.Add(leftVolumeBar.transform.GetChild(i).gameObject);
            rightSideCubes.Add(rightVolumeBar.transform.GetChild(i).gameObject);
        }

        _resetPos = new Vector3(0, 0, zSpawn);
        playerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        Tile.speed = tileSpeed;
        oldSpeed = tileSpeed;
        leftEndPos = leftSideCubes[leftSideCubes.Count - 1].transform.position;
        rightEndPos = rightSideCubes[rightSideCubes.Count - 1].transform.position;
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

        for(int i = 0; i < leftSideCubes.Count; i++)
        {
            leftSideCubes[i].transform.Translate(-Vector3.right * tileSpeed * Time.deltaTime);
            rightSideCubes[i].transform.Translate(-Vector3.right * tileSpeed * Time.deltaTime);

            if(leftSideCubes[i].transform.position.z < playerTransform.position.z - 2f)
            {
                leftSideCubes[i].transform.position = leftEndPos;
                rightSideCubes[i].transform.position = rightEndPos;
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
