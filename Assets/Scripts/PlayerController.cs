using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int score = 0;
    public int life = 3;

    private List<Transform> playerGrid;
    private bool isMoving = false;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private int actualPos;

    private Rigidbody m_rb;
    public Transform movePoint;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        movePoint.parent = null;
        playerGrid = GameObject.FindObjectOfType<PlayerGridManager>().playerGrid;
        minX = playerGrid[0].transform.position.x;
        maxX = playerGrid[2].transform.position.x;
        minY = playerGrid[7].transform.position.y;
        maxY = playerGrid[1].transform.position.y;
        actualPos = 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (transform.position.x < maxX)
                {
                    isMoving = true;
                    movePoint.position = playerGrid[actualPos + 1].transform.position;
                    actualPos += 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (transform.position.x > minX)
                {
                    isMoving = true;
                    movePoint.position = playerGrid[actualPos - 1].transform.position;
                    actualPos -= 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (transform.position.y < maxY)
                {
                    isMoving = true;
                    movePoint.position = playerGrid[actualPos - 3].transform.position;
                    actualPos -= 3;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (transform.position.y > minY)
                {
                    isMoving = true;
                    movePoint.position = playerGrid[actualPos + 3].transform.position;
                    actualPos += 3;
                }
            }
        }

        else
        {
            if(transform.position == playerGrid[actualPos].position)
            {
                isMoving = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            life -= 1;
        }

        if (other.CompareTag("Note"))
        {
            score += 10;
        }
    }
}
