using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float offset;
    public float moveSpeed;

    private Vector3 posLeft = new Vector3();
    private Vector3 posCenter = new Vector3();
    private Vector3 posRight = new Vector3();
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        posCenter = this.transform.position;
        posLeft = posCenter - new Vector3(offset, 0, 0);
        posRight = posCenter + new Vector3(offset, 0, 0);
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.actualPos == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, posLeft, moveSpeed * Time.deltaTime);
        }

        if (player.actualPos == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, posCenter, moveSpeed * Time.deltaTime);
        }

        if (player.actualPos == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, posRight, moveSpeed * Time.deltaTime);
        }
    }
}
