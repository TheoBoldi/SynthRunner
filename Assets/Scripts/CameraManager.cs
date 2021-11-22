using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float followSpeed = 5f;
    public float cameraOffset = 2.5f;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.side == Side.Center)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        }

        if(player.side == Side.Left)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-cameraOffset, transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        }

        if (player.side == Side.Right)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(cameraOffset, transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        }
    }
}
