using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridManager : MonoBehaviour
{
    [HideInInspector] public List<Transform> playerGrid;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < this.transform.childCount; i++) 
        {
            playerGrid.Add(transform.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
