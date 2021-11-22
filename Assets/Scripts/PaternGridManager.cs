using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum objectType { Wall, JumpOver, RollUnder, Void };

public class PaternGridManager : MonoBehaviour
{
    public float frequence;
    public float speed;
    public List<objectType> patern1;
    public List<objectType> patern2;
    public List<objectType> patern3;
    public List<objectType> patern4;
    public List<objectType> patern5;
    public List<objectType> patern6;
    public List<objectType> patern7;
    public List<objectType> patern8;
    public List<objectType> patern9;
    [HideInInspector] public List<GameObject> instanciatedObjects;
    [HideInInspector] public List<Transform> gridPositions;
    private List<objectType> actualParten;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            gridPositions.Add(transform.transform.GetChild(i));
        }

        StartCoroutine(InstanciatePatern());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            instanciatedObjects[i].transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    IEnumerator InstanciatePatern()
    {
        int rand = Random.Range(0, 9);
        if (rand == 0)
            actualParten = patern1;
        if (rand == 1)
            actualParten = patern2;
        if (rand == 2)
            actualParten = patern3;
        if (rand == 3)
            actualParten = patern4;
        if (rand == 4)
            actualParten = patern5;
        if (rand == 5)
            actualParten = patern6;
        if (rand == 6)
            actualParten = patern7;
        if (rand == 7)
            actualParten = patern8;
        if (rand == 8)
            actualParten = patern9;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if(actualParten[i] != objectType.Void)
            {
                var tmp = Resources.Load("Prefabs/" + actualParten[i].ToString());
                GameObject newGO = Instantiate(tmp as GameObject, gridPositions[i].position, gridPositions[i].rotation);
                instanciatedObjects.Add(newGO);
            }
        }

        yield return new WaitForSeconds(frequence);
        StartCoroutine(InstanciatePatern());
    }
}
