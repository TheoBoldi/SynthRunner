using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { Wall, JumpOver, RollUnder, Void };

[System.Serializable]
public class Patern
{
    public List<ObstacleType> patern;
    public List<int> compatiblePaternIndex;
}

[System.Serializable]
public class PaternList
{
    public List<Patern> paternsList;
}

public class PaternGenerator : MonoBehaviour
{
    public float frequence;
    public float speed;
    public PaternList ListOfPaterns = new PaternList();
    private static bool firstSpawn = false;
    private List<GameObject> instanciatedObjects = new List<GameObject>();
    private List<Transform> gridPositions = new List<Transform>();
    private List<ObstacleType> actualParten = new List<ObstacleType>();
    private List<int> nextPossibleIndex = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        firstSpawn = false;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            gridPositions.Add(transform.transform.GetChild(i));
        }

        StartCoroutine(InstanciatePatern());
    }

    // Update is called once per frame
    void Update()
    {
        frequence = MusicManager.Instance.GetActiveFrequency();
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            instanciatedObjects[i].transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    IEnumerator InstanciatePatern()
    {
        int rand;

        if (!firstSpawn)
        {
            rand = Random.Range(0, ListOfPaterns.paternsList.Count);
        }
        else
        {
            rand = nextPossibleIndex[Random.Range(0, nextPossibleIndex.Count)];
        }

        nextPossibleIndex = ListOfPaterns.paternsList[rand].compatiblePaternIndex;

        for (int i = 0; i < ListOfPaterns.paternsList[rand].patern.Count; i++)
        {
            actualParten.Add(ListOfPaterns.paternsList[rand].patern[i]);
        }

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if(actualParten[i] != ObstacleType.Void)
            {
                var tmp = Resources.Load("Prefabs/" + actualParten[i].ToString());
                GameObject newGO = Instantiate(tmp as GameObject, gridPositions[i].position, gridPositions[i].rotation);
                newGO.transform.parent = this.gameObject.transform;
                instanciatedObjects.Add(newGO);
            }
        }

        yield return new WaitForSeconds(frequence);
        actualParten.Clear();

        if (!firstSpawn)
            firstSpawn = true;

        StartCoroutine(InstanciatePatern());
    }
}
