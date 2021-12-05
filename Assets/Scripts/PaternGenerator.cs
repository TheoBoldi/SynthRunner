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
    private float _oldFrequence;
    private float timerfrequence;
    public float speed;
    public PaternList ListOfPaterns = new PaternList();
    private static bool firstSpawn = false;
    private List<GameObject> instanciatedObjects = new List<GameObject>();
    private List<Transform> gridPositions = new List<Transform>();
    private List<ObstacleType> actualParten = new List<ObstacleType>();
    private List<int> nextPossibleIndex = new List<int>();

    private bool patternGeneration = true;

    // Start is called before the first frame update
    void Start()
    {
        frequence = MusicManager.Instance.GetActiveFrequency();
        _oldFrequence = frequence;

        firstSpawn = false;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            gridPositions.Add(transform.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamePause || GameManager.Instance.isIntro) return;
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            instanciatedObjects[i].transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (GameManager.Instance.musicPause) return;

        if (patternGeneration)
        {
            if (MusicManager.Instance.GetActiveIntensity() == 0)
            {
                patternGeneration = false;
                return;
            }

            frequence = MusicManager.Instance.GetActiveFrequency();
            if (_oldFrequence != frequence)
            {
                MusicManager.Instance._tunnel.RotateShape(_oldFrequence > frequence);
                if (MusicManager.Instance.GetActiveIntensity() == 0)
                {
                    MusicManager.Instance._tunnel.RotateShape(false);
                }
                timerfrequence = 0;
                _oldFrequence = frequence;
            }
            timerfrequence += Time.deltaTime;
            if (timerfrequence >= frequence)
            {
                InstanciatePatern();
                timerfrequence = 0f;
            }
        }

        if (!patternGeneration && timerfrequence != 0)
            timerfrequence = 0;

        if (!patternGeneration && MusicManager.Instance.GetActiveIntensity() > 0)
            patternGeneration = true;

    }

    void InstanciatePatern()
    {
        int rand;
        actualParten.Clear();

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
            if(actualParten[i] != ObstacleType.Wall)
            {
                /*var tmp = Resources.Load("Prefabs/Note");
                GameObject newGO = Instantiate(tmp as GameObject, gridPositions[i].position, gridPositions[i].rotation);
                newGO.transform.parent = this.gameObject.transform;
                instanciatedObjects.Add(newGO);*/
            }
        }


        if (!firstSpawn)
            firstSpawn = true;
    }
}
