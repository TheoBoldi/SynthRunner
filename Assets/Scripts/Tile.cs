using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static float speed;

    void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }
}
