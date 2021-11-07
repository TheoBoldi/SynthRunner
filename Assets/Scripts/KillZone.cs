using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private PaternGridManager paternGridManager;

    // Start is called before the first frame update
    void Start()
    {
        paternGridManager = GameObject.FindObjectOfType<PaternGridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        paternGridManager.instanciatedObjects.Remove(other.gameObject);
        Destroy(other.gameObject);
    }
}
